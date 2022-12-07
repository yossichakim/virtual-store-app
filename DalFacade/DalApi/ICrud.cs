namespace DalApi;

/// <summary>
/// Declaration of an interface for the data entities (product, order, order items)
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICrud<T> where T : struct
{
    /// <summary>
    /// Adding an entity to list
    /// </summary>
    /// <param name="add"></param>
    /// <returns> the id of the entity </returns>
    public int Add(T add);

    /// <summary>
    /// The function receives an entity ID and returns it
    /// </summary>
    /// <param name="get"></param>
    /// <returns> entity from list </returns>
    public T Get(int get);

    /// <summary>
    /// The function accepts a condition and returns an entity if it meets the condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns>entity if it meets the condition</returns>
    public T Get(Func<T?, bool>? func);

    /// <summary>
    /// The function accepts a condition (not necessary), the function will return all entities,
    /// (if there is a condition, it will return only all those who meet the condition)
    /// </summary>
    /// <param name="func"></param>
    /// <returns>all entities,
    /// (if there is a condition, it will return only all those who meet the condition)</returns>
    public IEnumerable<T?> GetAll(Func<T?, bool>? func = null);

    /// <summary>
    /// Gets an entity ID and deletes it if it exists
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id);

    /// <summary>
    /// Gets an entity ID and updates it if it exists
    /// </summary>
    /// <param name="update"></param>
    public void Update(T update);
}