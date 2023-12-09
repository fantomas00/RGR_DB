using Rgr.Domain.Companies;
using Rgr.Domain.Core.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rgr.Domain.Medicines;

[Table("medicines")]
public sealed class Medicine : IEntity
{
    public Medicine() { }

    public Medicine(string name, long companyId)
    {
        Name = name;
        CompanyId = companyId;
    }

    [Key]
    [Column("medicine_id")]
    public long Id { get; init; }

    [Column("medicine_name")]
    public string Name { get; set; }

    [Column("company_id")]
    [ForeignKey("companies")]
    public long CompanyId { get; set; }
}
