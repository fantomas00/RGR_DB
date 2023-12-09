using Rgr.Domain.Core.Abstractions;

namespace Rgr.Domain.Patients;

public interface IPatientRepository : IRepository<Patient>
{
    long GetPharmacyCountById(long id);
}
