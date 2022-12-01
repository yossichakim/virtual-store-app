using System;

namespace DalApi;

public interface ICrud<T> where T : struct
{
    public int Add(T add);

    public T Get(int get);

    public IEnumerable<T?> GetAll(Func<T?, bool>? func = null);

    public void Delete(int id);

    public void Update(T update);
}