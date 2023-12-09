using Rgr.Domain.Medicines;
using RGR.Persistence.Core.Infrastructure;
using Spectre.Console;

namespace Rgr.Console.Controllers;

public class MedicineController : Controller<Medicine>
{
    public MedicineController(IMedicineRepository repository) : base(repository)
    {
    }

    public void CustomQuery()
    {
        string name = AnsiConsole.Prompt(
                    new TextPrompt<string>($"Enter like expression:")
                    .ValidationErrorMessage("Not valid value"));

        Table table = new Table();

        table.Title = new TableTitle(EntityMapper<Medicine>.GetTableName(), new(Color.DeepPink4));

        table.AddColumns([.. EntityMapper<Medicine>.GetColumnNames(true)
            .Select(c => new TableColumn(new Markup(c, Color.Purple4_1))),
            new TableColumn(new Markup("company_name", Color.Purple4_1))]);

        foreach (var entity in _repository.GetAll())
        {
            var values = typeof(Medicine).GetProperties()
                .Select(p => p.GetValue(entity)?.ToString() ?? "NULL");

            table.AddRow([.. values,
                ((IMedicineRepository)_repository).GetCompanyByName(entity.Id, name)?.Name ?? "NULL"]);
        }

        AnsiConsole.Write(table);
        AnsiConsole.Prompt(
                new TextPrompt<string>("Press to continue...")
                .AllowEmpty());
        AnsiConsole.Clear();
    }
}
