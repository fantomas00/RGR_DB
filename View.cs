using Npgsql;
using Rgr.Domain.Core.Primitives;
using RGR.Persistence.Core.Infrastructure;
using Spectre.Console;

namespace Rgr.Console;

public static class View<TEntity> where TEntity : IEntity, new()
{
    public static void PrintTable(IEnumerable<TEntity> entities)
    {
        Table table = new Table();

        table.Title = new TableTitle(EntityMapper<TEntity>.GetTableName(), new(Color.DeepPink4));

        table.AddColumns(EntityMapper<TEntity>.GetColumnNames(true)
            .Select(c => new TableColumn(new Markup(c, Color.Purple4_1))).ToArray());

        foreach (var entity in entities)
        {
            var values = typeof(TEntity).GetProperties()
                .Select(p => p.GetValue(entity)?.ToString() ?? "NULL");

            table.AddRow(values.ToArray());
        }

        AnsiConsole.Write(table);
        AnsiConsole.Prompt(
                new TextPrompt<string>("Press to continue...")
                .AllowEmpty());
        AnsiConsole.Clear();
    }

    public static void PrintCreate(TEntity entity)
    {
        AnsiConsole.Write(new Markup($"Created record in table {EntityMapper<TEntity>.GetTableName()}\n", new(Color.DeepPink4)));
        PrintTable([entity]);
        AnsiConsole.Prompt(
                new TextPrompt<string>("Press to continue...")
                .AllowEmpty());
        AnsiConsole.Clear();
    }

    public static void PrintUpdate(TEntity entity)
    {
        AnsiConsole.Write(new Markup($"Updated record in table {EntityMapper<TEntity>.GetTableName()} with id {entity.Id}\n", new(Color.DeepPink4)));
        PrintTable([entity]);
        AnsiConsole.Prompt(
                new TextPrompt<string>("Press to continue...")
                .AllowEmpty());
        AnsiConsole.Clear();
    }

    public static void PrintDelete(TEntity entity)
    {
        AnsiConsole.Write(new Markup($"Deleted record in table {EntityMapper<TEntity>.GetTableName()}\n", new(Color.DeepPink4)));
        PrintTable([entity]);
        AnsiConsole.Prompt(
                new TextPrompt<string>("Press to continue...")
                .AllowEmpty());
        AnsiConsole.Clear();
    }

    public static void PrintError(Exception exception)
    {
        AnsiConsole.Write(new Markup($"{exception.Message}\n", new(Color.Purple4_1)));
        AnsiConsole.Prompt(
                new TextPrompt<string>("Press to continue...")
                .AllowEmpty());
        AnsiConsole.Clear();
    }

    public static void PrintGenerate(int count)
    {
        AnsiConsole.Write(new Markup($"Generated {count} records in table {EntityMapper<TEntity>.GetTableName()}\n", new(Color.DeepPink4)));
        AnsiConsole.Prompt(
                new TextPrompt<string>("Press to continue...")
                .AllowEmpty());
        AnsiConsole.Clear();
    }
}
