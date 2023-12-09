using Rgr.Domain.Pharmacies;
using System.Data;

namespace RGR.Persistence.Repositories;

public class PharmacyRepository : Repository<Pharmacy>, IPharmacyRepository
{
    public PharmacyRepository(IDbConnection connection) : base(connection) {}

    public Task<long> GetPatientCountByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}
