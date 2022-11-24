using BO;

namespace BLApi;

public interface IOrder
{
    public IEnumerable<OrderForList> GetOrderList();

    public Order GetOrderDetails(int orderID);

    public Order ShippingUpdate(int orderID);

    public Order DeliveryUpdate(int orderID);

    public OrderTracking OrderTrackingManger(int orderID);

    ///BONUS///
    public void UpdateOrder(int orderID);
}