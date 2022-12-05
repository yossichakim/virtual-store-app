using DalApi;
using DO;
using System.Linq;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// Adding order items to the list of order items
    /// </summary>
    /// <param name="addOrderItem"></param>
    /// <returns></returns>
    /// <exception cref="AddException"> if the array of orders items are full </exception>
    public int Add(OrderItem addOrderItem)
    {
        addOrderItem.OrderItemID = DataSource.getOrderItemID;
        DataSource.orderItems.Add(addOrderItem);

        return addOrderItem.OrderItemID;
    }

    /// <summary>
    /// Find the order items by the ID number
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns> Returns the requested order item </returns>
    /// <exception cref="NoFoundException"> if the order item not exist </exception>
    public OrderItem Get(int orderItem)
    {
        return Get(element => element?.OrderItemID == orderItem);
    }

    /// <summary>
    /// <returns> Returns the list of order items in condition </returns>
    /// </summary>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func = null)
        => func is null ? DataSource.orderItems.Select(item => item):
         DataSource.orderItems.Where(func);


    /// <summary>
    /// Deletion of an order item by ID number of the order item
    /// </summary>
    /// <param name="orderItemID"></param>
    /// <exception cref="NoFoundException"> if the order item not exist </exception>
    public void Delete(int orderItemID)
    {
        if (!DataSource.orderItems.Exists(element => element?.OrderItemID == orderItemID))
            throw new NoFoundException("ORDER ITEM");

        DataSource.orderItems.RemoveAll(element => element?.OrderItemID == orderItemID);
    }

    /// <summary>
    /// Order item update of a requested product
    /// </summary>
    /// <param name="updateOrderItem"></param>
    /// <exception cref="NoFoundException"> if the order item not exist </exception>
    public void Update(OrderItem updateOrderItem)
    {
        Delete(updateOrderItem.OrderItemID);
        Add(updateOrderItem);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public OrderItem Get(Func<OrderItem?, bool>? func)
    {
        if (DataSource.orderItems.FirstOrDefault(func!) is OrderItem orderItem)
            return orderItem;

        throw new NoFoundException("ORDER ITEM");
    }
}