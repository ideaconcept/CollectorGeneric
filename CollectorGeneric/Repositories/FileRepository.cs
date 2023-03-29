using CollectorGeneric.Entities;
using System.Text.Json;

namespace CollectorGeneric.Repositories
{
    public class FileRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        protected List<T> _items = new();

        public event EventHandler<T>? ItemAdded, ItemRemove;

        public void LoadRepositiry()
        {
            _items = ReadRepoFromFile<T>();
        }

        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
            ItemAdded?.Invoke(this, item);
        }

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }

        public T GetById(int id)
        {
            return _items.Single(item => item.Id == id);
        }

        public void Remove(T item)
        {
            _items.Remove(item);
            ItemRemove?.Invoke(this, item);
        }

        public void Save()
        {
            SaveRepoToFile<T>(_items); ;
        }

        List<T> ReadRepoFromFile<T>()
            where T : class, IEntity, new()
        {
            T Object = new();
            var fileName = Object.GetType().Name + ".txt";
            List<T> objectList = new List<T>();

            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText(fileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        Object = JsonSerializer.Deserialize<T>(line);
                        if (Object != null)
                            objectList.Add(Object);
                        line = reader.ReadLine();
                    }
                }
            }
            return objectList.ToList();
        }

        private void SaveRepoToFile<T>(List<T> items)
            where T : class, IEntity, new()
        {
            T Object = new();
            var fileName = Object.GetType().Name + ".txt";

            using (var writer = File.CreateText(fileName))
            {
                int itemCount = this._items.Count;
                for (int i = 0; i < itemCount; i++)
                {
                    T item = items[i];
                    string jsonObj = JsonSerializer.Serialize<T>(item);
                    writer.WriteLine(jsonObj);
                }
            }
        }
    }
}
