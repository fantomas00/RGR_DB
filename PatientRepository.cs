using Dapper;
using Rgr.Domain.Patients;
using System.Data;

namespace RGR.Persistence.Repositories;

public class PatientRepository : Repository<Patient>, IPatientRepository
{
    public PatientRepository(IDbConnection connection) : base(connection) {}

    public long GetPharmacyCountById(long id)
    {
        string sql =
            $"""
                select
            	    count(pharmacy_id)
                from
            	    patients
                full join pharmacy_patients using (patient_id)
                where
            	    patient_id = 10
                group by
            	    patient_id 
            """;

        _connection.Open();

        long result;

        try
        {
            result = _connection.ExecuteScalar<long>(sql, new { id = id });
        }
        finally
        {
            _connection.Close();
        }

        return result;
    }
}
