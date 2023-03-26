using CollectorGeneric.Entities;
using CollectorGeneric.Repositories;

namespace CollectorGeneric.Repositories
{
    public interface IReadRepository<out T>
        where T : class, IEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
