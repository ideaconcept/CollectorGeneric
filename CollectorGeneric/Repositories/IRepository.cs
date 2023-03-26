using CollectorGeneric.Entities;
using CollectorGeneric.Repositories;

namespace CollectorGeneric.Repositories
{
    public interface IRepository<T> : IWriteRepository<T>, IReadRepository<T>
        where T : class, IEntity
    {
    }
}
