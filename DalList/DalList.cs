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
    private DalList() 
    {
        this.Product = new DalProduct();
        this.Order = new DalOrder();
        this.OrderItem = new DalOrderItem();
    }

    public static IDal Instance { get; } = new DalList();

    /// <summary>
    /// Interface for product list
    /// </summary>
    public IProduct Product { get; }

    /// <summary>
    /// Interface for order list
    /// </summary>
    public IOrder Order { get; }

    /// <summary>
    /// Interface for a list of order items
    /// </summary>
    public IOrderItem OrderItem { get; }    
}