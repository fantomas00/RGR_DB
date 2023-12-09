using Npgsql;
using Rgr.Console;
using Rgr.Console.Controllers;
using RGR.Persistence.Repositories;
using Spectre.Console;
using System.Text;

string connectionString = "host=localhost;port=5433;database=Rgr;user id=postgres;password=pass12345";
var connection = new NpgsqlConnection(connectionString);

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

var currentScene = runMainMenu();

while (currentScene != MainMenu.Exit)
{
    switch (runMainMenu())
    {
        case MainMenu.CompanyMenu:
            {
                CompanyController controller = new CompanyController(
                                new CompanyRepository(connection));
                switch (runTableMenuWithCustom())
                {
                    case TableMenu.GetAll:
                        {
                            controller.GetAll();
                        }
                        break;
                    case TableMenu.Add:
                        {
                            controller.Add();
                        }
                        break;
                    case TableMenu.Update:
                        {
                            controller.Update();
                        }
                        break;
                    case TableMenu.Delete:
                        {
                            controller.Delete();
                        }
                        break;
                    case TableMenu.Generate:
                        {
                            controller.Generate();
                        }
                        break;
                    case TableMenu.Back:
                        {
                            currentScene = MainMenu.CompanyMenu;
                        }
                        break;
                    default:
                        {
                            controller.CustomQuery();
                        }
                        break;
                }
            }
            break;
        case MainMenu.MedicineMenu:
            {
                MedicineController controller = new MedicineController(
                                new MedicineRepository(connection));
                switch (runTableMenuWithCustom())
                {
                    case TableMenu.GetAll:
                        {
                            controller.GetAll();
                        }
                        break;
                    case TableMenu.Add:
                        {
                            controller.Add();
                        }
                        break;
                    case TableMenu.Update:
                        {
                            controller.Update();
                        }
                        break;
                    case TableMenu.Delete:
                        {
                            controller.Delete();
                        }
                        break;
                    case TableMenu.Generate:
                        {
                            controller.Generate();
                        }
                        break;
                    case TableMenu.Back:
                        {
                            currentScene = MainMenu.MedicineMenu;
                        }
                        break;
                    default:
                        {
                            controller.CustomQuery();
                        }
                        break;
                }
            }
            break;
        case MainMenu.PatientMenu:
            {
                PatientController controller = new PatientController(
                                new PatientRepository(connection));
                switch (runTableMenuWithCustom())
                {
                    case TableMenu.GetAll:
                        {
                            controller.GetAll();
                        }
                        break;
                    case TableMenu.Add:
                        {
                            controller.Add();
                        }
                        break;
                    case TableMenu.Update:
                        {
                            controller.Update();
                        }
                        break;
                    case TableMenu.Delete:
                        {
                            controller.Delete();
                        }
                        break;
                    case TableMenu.Generate:
                        {
                            controller.Generate();
                        }
                        break;
                    case TableMenu.Back:
                        {
                            currentScene = MainMenu.PatientMenu;
                        }
                        break;
                    default:
                        {
                            controller.CustomQuery();
                        }
                        break;
                }
            }
            break;
        case MainMenu.PharmacyMenu:
            {
                PharmacyController controller = new PharmacyController(
                                new PharmacyRepository(connection));
                switch (runTableMenu())
                {
                    case TableMenu.GetAll:
                        {
                            controller.GetAll();
                        }
                        break;
                    case TableMenu.Add:
                        {
                            controller.Add();
                        }
                        break;
                    case TableMenu.Update:
                        {
                            controller.Update();
                        }
                        break;
                    case TableMenu.Delete:
                        {
                            controller.Delete();
                        }
                        break;
                    case TableMenu.Generate:
                        {
                            controller.Generate();
                        }
                        break;
                    case TableMenu.Back:
                        {
                            currentScene = MainMenu.PharmacyMenu;
                        }
                        break;
                }
            }
            break;
        case MainMenu.Exit:
            return;
    }
}

TableMenu runTableMenu()
{
    return AnsiConsole.Prompt(
        new SelectionPrompt<TableMenu>()
        {
            DisabledStyle = new(Color.SkyBlue1)
        }
        .HighlightStyle(new(Color.LightGoldenrod1))
        .Title("Select operation:")
        .AddChoices( [
            TableMenu.Add,
            TableMenu.GetAll,
            TableMenu.Update,
            TableMenu.Delete,
            TableMenu.Generate,
            TableMenu.Back
        ])
        );
}

TableMenu runTableMenuWithCustom()
{
    return AnsiConsole.Prompt(
        new SelectionPrompt<TableMenu>()
        {
            DisabledStyle = new(Color.SkyBlue1)
        }
        .HighlightStyle(new(Color.LightGoldenrod1))
        .Title("Select operation:")
        .AddChoices([
            TableMenu.Add,
            TableMenu.GetAll,
            TableMenu.Update,
            TableMenu.Delete,
            TableMenu.Generate,
            TableMenu.CustomQuery,
            TableMenu.Back
        ]));
}

MainMenu runMainMenu()
{
    return AnsiConsole.Prompt(
        new SelectionPrompt<MainMenu>()
        {
            DisabledStyle = new(Color.SkyBlue1)
        }
        .HighlightStyle(new(Color.LightGoldenrod1))
        .Title("Select table:")
        .AddChoices( [
            MainMenu.CompanyMenu,
            MainMenu.MedicineMenu,
            MainMenu.PatientMenu,
            MainMenu.PharmacyMenu,
            MainMenu.Exit
        ]));
}

return;
