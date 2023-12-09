using Rgr.Domain.Companies;
using RGR.Persistence.Core.Infrastructure;
using Spectre.Console;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rgr.Console.Controllers;

public class CompanyController : Controller<Company>
{
    public CompanyController(ICompanyRepository repository) : base(repository)
    {
    }

    public void CustomQuery()
    {
        long a = AnsiConsole.Prompt(
                    new TextPrompt<long>($"Enter first bound:")
                    .ValidationErrorMessage("Not valid value"));

        long b = AnsiConsole.Prompt(
                    new TextPrompt<long>($"Enter second bound:")
                    .Validate(id =>
                    {
                        if (id < a)
                            return ValidationResult.Error("Second bound less than first");
                        return ValidationResult.Success();
                    })
                    .ValidationErrorMessage("Not valid value"));


        Table table = new Table();

        table.Title = new TableTitle(EntityMapper<Company>.GetTableName(), new(Color.DeepPink4));

        table.AddColumns([..EntityMapper<Company>.GetColumnNames(true)
            .Select(c => new TableColumn(new Markup(c, Color.Purple4_1))),
            new TableColumn(new Markup("production_count", Color.Purple4_1))]);

        foreach (var entity in _repository.GetAll())
        {
            var values = typeof(Company).GetProperties()
                .Select(p => p.GetValue(entity)?.ToString() ?? "NULL");

            table.AddRow([..values, 
                ((ICompanyRepository)_repository).GetProductionCountInRange(entity.Id, a, b).ToString()]);
        }

        AnsiConsole.Write(table);
        AnsiConsole.Prompt(
                new TextPrompt<string>("Press to continue...")
                .AllowEmpty());
        AnsiConsole.Clear();
    }
}
