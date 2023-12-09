using Rgr.Domain.Patients;
using RGR.Persistence.Core.Infrastructure;
using Spectre.Console;

namespace Rgr.Console.Controllers;

public class PatientController : Controller<Patient>
{
    public PatientController(IPatientRepository repository) : base(repository)
    {
    }

    public void CustomQuery()
    {
        Table table = new Table();

        table.Title = new TableTitle(EntityMapper<Patient>.GetTableName(), new(Color.DeepPink4));

        table.AddColumns([.. EntityMapper<Patient>.GetColumnNames(true)
            .Select(c => new TableColumn(new Markup(c, Color.Purple4_1))),
            new TableColumn(new Markup("pharmacy_count", Color.Purple4_1))]);

        foreach (var entity in _repository.GetAll())
        {
            var values = typeof(Patient).GetProperties()
                .Select(p => p.GetValue(entity)?.ToString() ?? "NULL");

            table.AddRow([.. values,
                ((IPatientRepository)_repository).GetPharmacyCountById(entity.Id).ToString() ?? "NULL"]);
        }

        AnsiConsole.Write(table);
        AnsiConsole.Prompt(
                new TextPrompt<string>("Press to continue...")
                .AllowEmpty());
        AnsiConsole.Clear();
    }
}
