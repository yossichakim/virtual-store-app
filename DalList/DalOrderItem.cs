using DO;
using DalApi;
namespace Dal;

internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// Adding order items to the list of order items
    /// </summary>
    /// <param name="addOrderItem"></param>
    /// <returns></returns>
    /// <exception cref="Exception"> if the array of orders items are full </exception>
    public int Add(OrderItem addOrderItem)
    {
        if (DataSource.orderItems.Exists(element => element.OrderItemID == addOrderItem.OrderID))
            throw new AddIsExists("order item");

        DataSource.orderItems.Add(addOrderItem);

        return addOrderItem.OrderItemID;
    }

    /// <summary>
    /// Find the order items by the ID number
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns> Returns the requested order item </returns>
    /// <exception cref="Exception"> if the order item not exist </exception>
    public OrderItem Get(int orderItem)
    {
        if (!DataSource.orderItems.Exists(element => element.OrderItemID == orderItem))
            throw new EntityOrIDNoFound("order item");

        OrderItem returnOrderItem = new OrderItem();

        foreach (var tmpOrderItem in DataSource.orderItems)
        {
            if (tmpOrderItem.OrderItemID == orderItem)
            {
                returnOrderItem = tmpOrderItem;
            }
        }

        return returnOrderItem;
    }

    /// <summary>
    /// <returns> Returns the list of order items </returns>
    /// </summary>
    public IEnumerable<OrderItem> GetAll()
    {
        List<OrderItem> returnOrderItems = new();

        foreach (var item in DataSource.orderItems)
        {
            returnOrderItems.Add(item);
        }

        return returnOrderItems;
    }

    /// <summary>
    /// Deletion of an order item by ID number of the order item
    /// </summary>
    /// <param name="orderItemID"></param>
    /// <exception cref="Exception"> if the order item not exist </exception>
    public void Delete(int orderItemID)
    {
        if (!DataSource.orderItems.Exists(element => element.OrderItemID == orderItemID))
            throw new EntityOrIDNoFound("order item");

        DataSource.orderItems.RemoveAll(element => element.OrderItemID == orderItemID);

        //return;
    }

    /// <summary>
    /// Order item update of a requested product
    /// </summary>
    /// <param name="updateOrderItem"></param>
    /// <exception cref="Exception"> if the order item not exist </exception>
    public void Update(OrderItem updateOrderItem)
    {
        if (!DataSource.orderItems.Exists(element => element.OrderItemID == updateOrderItem.OrderItemID))
            throw new Exception("the order items you try to update are not exist");

        int index = 0;
        foreach (var item in DataSource.orderItems)
        {
            if (updateOrderItem.OrderItemID == item.OrderItemID)
            {
                DataSource.orderItems[index] = updateOrderItem;
                return;
            }
            index++;
        }
    }

    /// <summary>
    /// The function receives an ID number of the order and of a product
    /// and returns the corresponding order item
    /// </summary>
    /// <param name="prodID"></param>
    /// <param name="orderID"></param>
    /// <returns> returns the corresponding order item </returns>
    /// <exception cref="Exception"> if the order item not exist </exception>
    public OrderItem Find(int prodID, int orderID)
    {
        OrderItem returnOrderItem = new();
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
        {
            if (DataSource.orderItems[i].ProductID == prodID && DataSource.orderItems[i].OrderID == orderID)
            {
                returnOrderItem = DataSource.orderItems[i];
                return returnOrderItem;
            }
        }
        throw new EntityOrIDNoFound("order item");
    }

    /// <summary>
    /// Looking for all order items of an order
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns> Returns all order items of the same order ID number </returns>
    /// <exception cref="Exception"> if the order item not exist </exception>
    public IEnumerable<OrderItem> GetByOrderID(int orderID)
    {
        if (!DataSource.orderItems.Exists( element => element.OrderID == orderID))
            throw new EntityOrIDNoFound("order id");

        return DataSource.orderItems.Where(element => element.OrderID == orderID).ToList();
    }
}