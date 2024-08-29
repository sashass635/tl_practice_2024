namespace Domain.Repositories;

public interface IRepository<T> where T : class
{
    public void Add( T item );
    public void Remove( T item );
}