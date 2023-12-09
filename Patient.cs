using Rgr.Domain.Core.Primitives;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rgr.Domain.Patients;

[Table("patients")]
public sealed class Patient : IEntity
{
    public Patient() { }

    public Patient(string fullName, int age)
    {
        FullName = fullName;
        Age = age;
    }

    [Key]
    [Column("patient_id")]
    public long Id { get; init; }

    [Column("fullname")]
    public string FullName { get; set; }

    [Column("age")]
    public int Age { get; set; }
}
