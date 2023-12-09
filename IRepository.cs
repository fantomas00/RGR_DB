using Rgr.Domain.Core.Primitives;

namespace Rgr.Domain.Core.Abstractions;

public interface IRepository<TEntity> where TEntity : IEntity
{
    IEnumerable<TEntity> GetAll();

    void Add(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    void Generate(long count);
}
