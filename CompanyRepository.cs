using Dapper;
using Rgr.Domain.Companies;
using System.Data;

namespace RGR.Persistence.Repositories;

public class CompanyRepository : Repository<Company>, ICompanyRepository
{
    public CompanyRepository(IDbConnection connection) : base(connection) {}

    public long GetProductionCountInRange(long id, long a, long b)
    {
        string sql =
            """
                select
                    count(medicine_id) 
                from 
                    companies
                full join medicines using (company_id)
                where
                    company_id = @id
                having
                    count(medicine_id) between @a and @b
                order by
                    count(medicine_id) 
            """;

        _connection.Open();

        long result;

        try
        {
            result = _connection.ExecuteScalar<long>(sql, new { id = id, a = a, b = b });
        }
        finally
        {
            _connection.Close();
        }

        return result;
    }
}
