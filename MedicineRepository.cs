using Dapper;
using Rgr.Domain.Companies;
using Rgr.Domain.Medicines;
using System.Data;

namespace RGR.Persistence.Repositories;

public class MedicineRepository : Repository<Medicine>, IMedicineRepository
{
    public MedicineRepository(IDbConnection connection) : base(connection) {}

    public Company GetCompanyByName(long id, string name)
    {
        string sql =
            $"""
                select 
                    companies.company_id as Id,
                    company_name as Name,
                    country as Country,
                    owner as Owner
                from 
                    medicines 
                full join companies using (company_id)
                where
                    company_name like 'W%'
                order by
                    companies.company_id
            """;

        _connection.Open();

        Company result;

        try
        {
            result = _connection.QueryFirst<Company>(sql, new { id = id });
        }
        finally
        {
            _connection.Close();
        }

        return result;
    }
}
