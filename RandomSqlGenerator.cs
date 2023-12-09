using Rgr.Domain.Core.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace RGR.Persistence.Core.Infrastructure;

public class RandomSqlGenerator<TEntity> : IRandomSqlGenerator where TEntity : IEntity, new()
{
    private readonly int _stringLenght;

    private readonly int _maxIntValue;

    public RandomSqlGenerator(int stringLenght, int maxIntValue)
    {
        _stringLenght = stringLenght;
        _maxIntValue = maxIntValue;
    }

    protected string RandomStringSql(long lenght)
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < lenght; i++)
        {
            stringBuilder.Append("chr(trunc(65 + random() * 25)::int)");

            if (i != lenght - 1)
                stringBuilder.Append(" || ");
        }

        return stringBuilder.ToString();
    }

    protected string RandomIntSql(int maxValue) =>
        $"1 + random() * {maxValue}";

    protected string RandomForeignKeySql(string tableName, string keyColumnName) =>
        $"""
            (select foreigh_id 
            from 
                trunc(1 + random() * (select max({keyColumnName}) 
            from {tableName})) as foreigh_id 
            inner join {tableName} on foreigh_id = {tableName}.{keyColumnName})::bigint
        
        """;

    public string GenerateSql()
    {
        Type type = typeof(TEntity);

        return string.Join(", ", type.GetProperties()
            .Where(p => p.GetCustomAttribute<KeyAttribute>() == null)
            .Select(p =>
        {
            var foreignKey = p.GetCustomAttribute<ForeignKeyAttribute>();

            if (foreignKey != null)
            {
                return RandomForeignKeySql(foreignKey.Name, p.GetCustomAttribute<ColumnAttribute>()?.Name ?? p.Name);
            }

            return p.PropertyType.Name switch
            {
                nameof(Int32) => RandomIntSql(_maxIntValue),
                nameof(String) => RandomStringSql(_stringLenght),
                _ => throw new InvalidDataException("Cannot generate value of this type.")
            };
        }));
    }
}
