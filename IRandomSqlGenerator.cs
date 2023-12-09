using Rgr.Domain.Core.Primitives;

namespace RGR.Persistence.Core.Infrastructure;

public interface IRandomSqlGenerator
{
    string GenerateSql();
}
