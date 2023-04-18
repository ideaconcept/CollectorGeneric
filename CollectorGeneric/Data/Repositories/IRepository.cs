using CollectorGeneric.Data.Entities;

namespace CollectorGeneric.Data.Repositories
{
    public interface IRepository<T> : IWriteRepository<T>, IReadRepository<T>
        where T : class, IEntity
    {
        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;

        void LoadRepository();
    }
}
