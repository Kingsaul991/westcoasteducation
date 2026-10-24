namespace WestcoastEducation.Interfaces;
 
public interface IRepository<T> where T : class, IIdentifiable
{
    IReadOnlyList<T> GetAll();
    T? Find(string key);
    bool Add(T item);
    bool Remove(string key);
}
