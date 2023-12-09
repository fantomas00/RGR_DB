using Rgr.Domain.Core.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace RGR.Persistence.Core.Infrastructure;

public static class EntityMapper<TEntity> where TEntity : IEntity, new()
{
    public static IEnumerable<string> GetColumnNames(bool withPrimary = false)
    {
        IEnumerable<PropertyInfo> properties = typeof(TEntity).GetProperties();

        if (!withPrimary)
        {
            properties = properties.Where(p => p.GetCustomAttribute<KeyAttribute>() == null);
        }

        return properties.Select(p => p.GetCustomAttribute<ColumnAttribute>()?.Name ?? p.Name);
    }

    public static IEnumerable<string> GetParameterNames(bool withPrimary = false)
    {
        IEnumerable<PropertyInfo> properties = typeof(TEntity).GetProperties();

        if (!withPrimary)
        {
            properties = properties.Where(p => p.GetCustomAttribute<KeyAttribute>() == null);
        }

        return properties.Select(p => "@" + p.Name);
    }

    public static IEnumerable<string> GetPrimaryKeyNames() =>
        typeof(TEntity).GetProperties()
            .Where(p => p.GetCustomAttribute<KeyAttribute>() != null)
            .Select(p => p.GetCustomAttribute<ColumnAttribute>()?.Name ?? p.Name);

    public static IEnumerable<string> GetForeignKeyNames() =>
        typeof(TEntity).GetProperties()
            .Where(p => p.GetCustomAttribute<ForeignKeyAttribute>() != null)
            .Select(p => p.GetCustomAttribute<ColumnAttribute>()?.Name ?? p.Name);

    public static string GetTableName() =>
        typeof(TEntity).GetCustomAttribute<TableAttribute>()?.Name ?? typeof(string).Name;

    public static IEnumerable<string> GetPropertyNames(bool withPrimary = false)
    {
        IEnumerable<PropertyInfo> properties = typeof(TEntity).GetProperties();

        if (!withPrimary)
        {
            properties = properties.Where(p => p.GetCustomAttribute<KeyAttribute>() == null);
        }

        return properties.Select(p => p.Name);

    }
        
}
