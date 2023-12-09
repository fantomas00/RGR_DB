using Dapper;
using Rgr.Domain.Core.Abstractions;
using Rgr.Domain.Core.Primitives;
using RGR.Persistence.Core.Infrastructure;
using System.Data;
using System.Linq;
using static Dapper.SqlMapper;

namespace RGR.Persistence.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity, new()
{
    protected readonly IDbConnection _connection;

    protected readonly IRandomSqlGenerator _sqlGenerator;

    public Repository(IDbConnection connection)
    {
        _connection = connection;
        _sqlGenerator = new RandomSqlGenerator<TEntity>(10, 100);
    }

    protected string ColumnsString(bool hasPrimaryKeys = false) =>
        string.Join(", ", EntityMapper<TEntity>.GetColumnNames(hasPrimaryKeys));

    protected string ParametersString(bool hasPrimaryKeys = false) =>
        string.Join(", ", EntityMapper<TEntity>.GetParameterNames(hasPrimaryKeys));

    protected string ColumnAsString(bool hasPrimaryKeys = false) =>
        string.Join(", ", EntityMapper<TEntity>.GetColumnNames(hasPrimaryKeys)
            .Zip(EntityMapper<TEntity>.GetPropertyNames(hasPrimaryKeys), (column, prop) => new { column, prop })
            .Select(e => e.column + " as " + e.prop));

    protected string GetParametrColumnString(bool hasPrimaryKeys = false) =>
        string.Join(", ", EntityMapper<TEntity>.GetParameterNames(hasPrimaryKeys)
            .Zip(EntityMapper<TEntity>.GetColumnNames(hasPrimaryKeys), (param, column) => new { param, column })
            .Select(e => e.column + " = " + e.param));

    public void Add(TEntity entity)
    {
        string sql = 
            $"""
                Insert into {EntityMapper<TEntity>.GetTableName()}({ColumnsString()})
                values({ParametersString()})
            """;

        _connection.Open();

        try
        {
            _connection.Execute(sql, entity);
        }
        finally
        {
            _connection.Close();
        }
    }

    public void Delete(TEntity entity)
    {
        string sql =
            $"""
                Delete from {EntityMapper<TEntity>.GetTableName()}
                where {EntityMapper<TEntity>.GetPrimaryKeyNames().FirstOrDefault()} = @Id
            """;

        _connection.Open();

        try
        {
            _connection.Execute(sql, new { entity.Id });
        }
        finally
        {
            _connection.Close();
        }
    }

    public void Update(TEntity entity)
    {
        string sql =
           $"""
                update {EntityMapper<TEntity>.GetTableName()} 
                set {GetParametrColumnString()}
                where {EntityMapper<TEntity>.GetPrimaryKeyNames().FirstOrDefault()} = @Id
            """;

        _connection.Open();

        try
        {
            _connection.Execute(sql, entity);
        }
        finally
        {
            _connection.Close();
        }
    }

    public IEnumerable<TEntity> GetAll()
    {
        string sql =
           $"""
                select {ColumnAsString(true)} from {EntityMapper<TEntity>.GetTableName()}
            """;

        _connection.Open();

        IEnumerable<TEntity> result;

        try
        {
            result = _connection.Query<TEntity>(sql);
        }
        finally
        { 
            _connection.Close();
        }

        return result;
    }

    public void Generate(long count)
    {
        string sql =
            $"""
                Insert into {EntityMapper<TEntity>.GetTableName()}({ColumnsString()})
                values({_sqlGenerator.GenerateSql()})
            """;

        _connection.Open();

        try
        {
            for (int i = 0; i < count; i++)
                _connection.Execute(sql);
        }
        finally
        {
            _connection.Close();
        }
    }
}
