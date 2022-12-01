using BO;

namespace BLApi;

/// <summary>
/// Order entity interface
/// </summary>
public interface IOrder
{
    /// <summary>
    /// The function returns a list of all orders
    /// </summary>
    /// <returns>returns a list of all orders</returns>
    public IEnumerable<OrderForList?> GetOrderList();

    /// <summary>
    /// The function receives an order ID and returns the order
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns>returns the order</returns>
    public Order GetOrderDetails(int orderID);

    /// <summary>
    /// The function receives an order ID and updates a shipping date
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns>Returns an order with an updated shipping date</returns>
    public Order ShippingUpdate(int orderID);

    /// <summary>
    /// The function receives an order ID and updates a date of delivery
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns>Returns an order with an updated delivery date</returns>
    public Order DeliveryUpdate(int orderID);

    /// <summary>
    /// The function receives an order ID and displays the order status
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns>Returns an order tracking entity with the order details</returns>
    public OrderTracking OrderTrackingManger(int orderID);
}