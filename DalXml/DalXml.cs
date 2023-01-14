namespace Dal;
using DalApi;

sealed internal class DalXml : IDal
{
    private DalXml() { }

    public static IDal Instance { get; } = new DalXml();

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
