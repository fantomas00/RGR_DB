using Rgr.Domain.Core.Abstractions;

namespace Rgr.Domain.Companies;

public interface ICompanyRepository : IRepository<Company>
{
    long GetProductionCountInRange(long id, long a, long b);
}
