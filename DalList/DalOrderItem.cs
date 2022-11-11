using DO;

namespace Dal;

public class DalOrderItem
{
    /// <summary>
    /// Adding order items to the list of order items
    /// </summary>
    /// <param name="addOrderItem"></param>
    /// <returns></returns>
    /// <exception cref="Exception"> if the array of orders items are full </exception>
    public int AddOrderItem(OrderItem addOrderItem)
    {
        if (DataSource.indexOrdersItems == DataSource.orderItems.Length)
            throw new Exception("no more space to add a new Order Items");

        DataSource.orderItems[DataSource.indexOrdersItems++] = addOrderItem;

        return addOrderItem.OrderItemID;
    }

    /// <summary>
    /// Find the order items by the ID number
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns> Returns the requested order item </returns>
    /// <exception cref="Exception"> if the order item not exist </exception>
    public OrderItem GetOrderItem(int orderItem)
    {
        if (!Array.Exists(DataSource.orderItems, element => element.OrderItemID == orderItem))
            throw new Exception("the order Item you try to get are not exist");

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
    public OrderItem[] GetAllOrdersItems()
    {
        OrderItem[] returnOrderItemsArr = new OrderItem[DataSource.indexOrdersItems];

        //DataSource.orderItems.CopyTo(returnOrderItemsArr, 0);
        for (int i = 0; i < returnOrderItemsArr.Length; i++)
        {
            returnOrderItemsArr[i] = DataSource.orderItems[i];
        }

        return returnOrderItemsArr;//.Select();
    }

    /// <summary>
    /// Deletion of an order item by ID number of the order item
    /// </summary>
    /// <param name="orderItemId"></param>
    /// <exception cref="Exception"> if the order item not exist </exception>
    public void RemoveOrderItem(int orderItemId)
    {
        if (!Array.Exists(DataSource.orderItems, element => element.OrderItemID == orderItemId))
            throw new Exception("the order Item you try to delete are not exist");

        for (int i = 0; i < DataSource.indexOrdersItems; i++)
        {
            if (DataSource.orderItems[i].OrderItemID == orderItemId)
            {
                if (i == DataSource.indexOrdersItems - 1)
                {
                    DataSource.orderItems[i] = new OrderItem();
                    DataSource.indexOrdersItems--;
                    return;
                }
                DataSource.orderItems[i] = DataSource.orderItems[--DataSource.indexOrdersItems];
                return;
            }
        }
    }

    /// <summary>
    /// Order item update of a requested product
    /// </summary>
    /// <param name="updateOrderItem"></param>
    /// <exception cref="Exception"> if the order item not exist </exception>
    public void UpdateOrderItem(OrderItem updateOrderItem)
    {
        if (!Array.Exists(DataSource.orderItems, element => element.OrderItemID == updateOrderItem.OrderItemID))
            throw new Exception("the order items you try to update are not exist");

        for (int i = 0; i < DataSource.indexOrdersItems; i++)
        {
            if (updateOrderItem.OrderItemID == DataSource.orderItems[i].OrderItemID)
            {
                DataSource.orderItems[i] = updateOrderItem;
                return;
            }
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
    public OrderItem FindOrderItem(int prodID,int orderID)
    {
        OrderItem returnOrderItem = new OrderItem();
        for (int i = 0; i < DataSource.indexOrdersItems; i++)
        {
            if (DataSource.orderItems[i].ProductID == prodID && DataSource.orderItems[i].OrderID == orderID)
            {
                returnOrderItem = DataSource.orderItems[i];
                return returnOrderItem;
            }
        }
        throw new Exception("the item you search are not found");
    }

    /// <summary>
    /// Looking for all order items of an order
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns> Returns all order items of the same order ID number </returns>
    /// <exception cref="Exception"> if the order item not exist </exception>
    public OrderItem[] GetByOrderID(int orderID)
    {
        if (!Array.Exists(DataSource.orderItems, element => element.OrderID == orderID))
            throw new Exception("the order item you try to get are not exist");

        return DataSource.orderItems.Where(element => element.OrderID == orderID).ToArray();
    }
}