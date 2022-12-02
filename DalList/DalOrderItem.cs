using DalApi;
using DO;

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
        if (!DataSource.orderItems.Exists(element => element?.OrderItemID == orderItem))
            throw new NoFoundException("order item");

        OrderItem? returnOrderItem = new OrderItem();

        foreach (var tmpOrderItem in DataSource.orderItems)
        {
            if (tmpOrderItem?.OrderItemID == orderItem)
            {
                returnOrderItem = tmpOrderItem;
            }
        }

        return (OrderItem)returnOrderItem;
    }

    /// <summary>
    /// <returns> Returns the list of order items </returns>
    /// </summary>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func = null)
    {
        return DataSource.orderItems.Select(item => item);
    }

    /// <summary>
    /// Deletion of an order item by ID number of the order item
    /// </summary>
    /// <param name="orderItemID"></param>
    /// <exception cref="NoFoundException"> if the order item not exist </exception>
    public void Delete(int orderItemID)
    {
        if (!DataSource.orderItems.Exists(element => element?.OrderItemID == orderItemID))
            throw new NoFoundException("order item");

        DataSource.orderItems.RemoveAll(element => element?.OrderItemID == orderItemID);

        //return;
    }

    /// <summary>
    /// Order item update of a requested product
    /// </summary>
    /// <param name="updateOrderItem"></param>
    /// <exception cref="NoFoundException"> if the order item not exist </exception>
    public void Update(OrderItem updateOrderItem)
    {
        if (!DataSource.orderItems.Exists(element => element?.OrderItemID == updateOrderItem.OrderItemID))
            throw new NoFoundException("order item");

        int index = 0;
        foreach (var item in DataSource.orderItems)
        {
            if (updateOrderItem.OrderItemID == item?.OrderItemID)
            {
                DataSource.orderItems[index] = updateOrderItem;
                return;
            }
            index++;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public OrderItem Get(Func<OrderItem?, bool>? func)
    {
        throw new NotImplementedException();
    }
}