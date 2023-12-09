using Rgr.Domain.Core.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rgr.Domain.Pharmacies;

[Table("pharmacies")]
public class Pharmacy : IEntity
{
    public Pharmacy() { }

    public Pharmacy(string name, string city)
    {
        Name = name;
        City = city;
    }

    [Key]
    [Column("pharmacy_id")]
    public long Id { get; init; }

    [Column("pharmacy_name")]
    public string Name { get; set; }

    [Column("city")]
    public string City { get; set; }
}
