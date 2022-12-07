using DalApi;

namespace Dal;

/// <summary>
/// A class for the entities that inherit from the interface
/// </summary>
public sealed class DalList : IDal
{
    public IProduct Product => new DalProduct();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
}