using DalApi;
namespace Dal;

/// <summary>
/// A class for the entities that inherit from the interface
/// </summary>
internal sealed class DalList : IDal
{
    /// <summary>
    /// constractor
    /// </summary>
    private DalList(){}

    public static IDal Instance { get; } = new DalList();

    /// <summary>
    /// Interface for product list
    /// </summary>
    public IProduct Product { get; } = new DalProduct();

    /// <summary>
    /// Interface for order list
    /// </summary>
    public IOrder Order { get; } = new DalOrder();

    /// <summary>
    /// Interface for a list of order items
    /// </summary>
    public IOrderItem OrderItem { get; } = new DalOrderItem();
}