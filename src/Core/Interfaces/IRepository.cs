namespace Core.Interfaces;

public interface IRepository<T>
{
    IEnumerable<T> FindAll();
    T FindById(double accountId);
    void Insert(T entity);
    Task SaveAsync();
}