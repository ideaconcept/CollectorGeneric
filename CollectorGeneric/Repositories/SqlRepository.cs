using CollectorGeneric.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollectorGeneric.Repositories
{
    public class SqlRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly DbSet<T> _dbSet;
        private readonly DbContext _dbContext;
        private readonly Action<T>? _itemEventCallback;

        public SqlRepository(DbContext dbContext, Action<T>? itemEventCalback = null)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _itemEventCallback = itemEventCalback;
        }

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemove;

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
            ItemAdded?.Invoke(this, item);
        }

        public void Remove(T item)
        {
            _dbSet.Remove(item);
            ItemRemove?.Invoke(this, item);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
