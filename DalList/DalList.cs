using DalApi;

namespace Dal;

/// <summary>
/// A class for the entities that inherit from the interface
/// </summary>
internal sealed class DalList : IDal
{
    private DalList() { }
    public static IDal Instance { get; } = new DalList();
    public IProduct Product => new DalProduct();
    public IOrder Order => new DalOrder();
    public IOrderItem OrderItem => new DalOrderItem();
}