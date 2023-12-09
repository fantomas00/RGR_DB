using Rgr.Domain.Companies;
using Rgr.Domain.Core.Abstractions;

namespace Rgr.Domain.Medicines;

public interface IMedicineRepository : IRepository<Medicine>
{
    Company GetCompanyByName(long id, string name);
}
