using Rgr.Domain.Core.Abstractions;
using Rgr.Domain.Core.Primitives;
using Spectre.Console;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using ValidationResult = Spectre.Console.ValidationResult;

namespace Rgr.Console.Controllers;

public class Controller<TEntity> where TEntity : IEntity, new()
{
    protected readonly IRepository<TEntity> _repository;

    public Controller(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public void GetAll()
    {
        try
        {
            View<TEntity>.PrintTable(_repository.GetAll());
        }
        catch (Exception ex)
        {
            View<TEntity>.PrintError(ex);
        }
    }

    public void Add()
    {
        TEntity entity = new TEntity();

        foreach (var prop in typeof(TEntity).GetProperties())
        {
            if (prop.GetCustomAttribute<KeyAttribute>() != null)
                continue;

            var converter = TypeDescriptor.GetConverter(prop.PropertyType);

            object? result;

            do
            {
                var entered = AnsiConsole.Prompt(
                    new TextPrompt<string>($"Enter {prop.GetCustomAttribute<ColumnAttribute>()?.Name ?? prop.Name}:")
                    .ValidationErrorMessage("Not valid value"));

                try
                {
                    result = converter.ConvertFrom(entered);
                    break;
                }
                catch
                {
                    AnsiConsole.WriteLine("Not valid value type");
                    continue;
                }
            } 
            while(true);

            prop.SetValue(entity, result);
        }

        try
        {
            _repository.Add(entity);
            View<TEntity>.PrintCreate(entity);
        }
        catch (Exception ex)
        {
            View<TEntity>.PrintError(ex);
        }
    }

    public void Update()
    {
        TEntity entity = new TEntity();

        foreach (var prop in typeof(TEntity).GetProperties())
        {
            var converter = TypeDescriptor.GetConverter(prop.PropertyType);

            object? result;

            do
            {
                var entered = AnsiConsole.Prompt(
                    new TextPrompt<string>($"Enter {prop.GetCustomAttribute<ColumnAttribute>()?.Name ?? prop.Name}:")
                    .ValidationErrorMessage("Not valid value"));

                try
                {
                    result = converter.ConvertFrom(entered);
                    break;
                }
                catch
                {
                    AnsiConsole.WriteLine("Not valid value type");
                    continue;
                }
            }
            while (true);

            prop.SetValue(entity, result);
        }

        try
        {
            _repository.Update(entity);
            View<TEntity>.PrintUpdate(entity);
        }
        catch (Exception ex)
        {
            View<TEntity>.PrintError(ex);
        }
    }

    public void Delete()
    {
        TEntity entity = new TEntity() { Id = AnsiConsole.Prompt(
                    new TextPrompt<long>($"Enter id:")
                    .ValidationErrorMessage("Not valid value")
                     )
        };

        try
        {
            _repository.Delete(entity);
            View<TEntity>.PrintDelete(entity);
        }
        catch (Exception ex)
        {
            View<TEntity>.PrintError(ex);
        }
    }

    public void Generate()
    {
        int count = AnsiConsole.Prompt(
                    new TextPrompt<int>("Enter count to generate")
                    .ValidationErrorMessage("Not valid value"));

        try
        {
            _repository.Generate(count);

            View<TEntity>.PrintGenerate(count);
        }
        catch (Exception ex)
        {
            View<TEntity>.PrintError(ex);
        }
    }
}
