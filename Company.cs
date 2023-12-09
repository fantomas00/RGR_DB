using Rgr.Domain.Core.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rgr.Domain.Companies;

[Table("companies")]
public sealed class Company : IEntity
{
    public Company() { }

    public Company(string name, string country, string owner)
    {
        Name = name;
        Country = country;
        Owner = owner;
    }

    [Key]
    [Column("company_id")]
    public long Id { get; init; }

    [Column("company_name")]
    public string Name { get; set; }

    [Column("country")]
    public string Country { get; set; }

    [Column("owner")]
    public string Owner { get; set; }
}
