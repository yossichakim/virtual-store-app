using DO;

namespace DalApi;

public interface ICrud<T>
{
    public int Add(T add);
    public T Get(int get);
    public IEnumerable<T> GetAll();
    public void Delete(int id);
    public void Update(T update);
}

