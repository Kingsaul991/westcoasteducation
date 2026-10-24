using WestcoastEducation.Interfaces;
 
namespace WestcoastEducation.Services;
 
public class Repository<T> : IRepository<T> where T : class, IIdentifiable

{
    private readonly List<T> _items = new();
    public IReadOnlyList<T> GetAll() => _items;
    public T? Find(string key)

    {

        foreach (var item in _items)

        {

            if (item.Key == key)

            {

                return item;

            }

        }
 
        return null;

    }

    public bool Add(T item)

    {

        if (string.IsNullOrWhiteSpace(item.Key) || Find(item.Key) != null)

        {

            return false;

        }
 
        _items.Add(item);

        return true;

    }
 
    public bool Remove(string key)

    {

        var item = Find(key);

        return item != null && _items.Remove(item);

    }

}
 