using DO;
using System.Linq;

namespace Dal;

public class DalOrderItem
{
    /// <summary>
    /// Adding order items to the list of order items
    /// </summary>
    /// <param name="addOrderItem"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int AddOrderItem(OrderItem addOrderItem)
    {

        if (DataSource.Config._indexOrdersItems == DataSource.orderItems.Length)
            throw new Exception("no more space to add a new Order Items");


        DataSource.orderItems[DataSource.Config._indexOrdersItems++] = addOrderItem;

        return addOrderItem.OrderItemID;

    }

    /// <summary>
    /// Find the order items by the ID number
    /// </summary>
    /// <param name="_orderItem"></param>
    /// <returns> Returns the requested order item </returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetOrderItem(int _orderItem)
    {
        if (!Array.Exists(DataSource.orderItems, element => element.OrderItemID == _orderItem))
            throw new Exception("the order Item you try to get are not exist");

        OrderItem returnOrderItem = new OrderItem();

        foreach (var tmpOrderItem in DataSource.orderItems)
        {
            if (tmpOrderItem.OrderItemID == _orderItem)
            {
                returnOrderItem = tmpOrderItem;
            }
        }

        return returnOrderItem;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns> Returns the list of order items </returns>
    public OrderItem[] GetAllOrdersItems()
    {
        OrderItem[] orderItems = new OrderItem[DataSource.Config._indexOrdersItems];

        DataSource.orderItems.CopyTo(orderItems, 0);

        return orderItems;
    }

    /// <summary>
    /// Deletion of an order item by ID number of the order item
    /// </summary>
    /// <param name="_orderItemId"></param>
    /// <exception cref="Exception"></exception>
    public void RemoveOrderItem(int _orderItemId)
    {
        if (!Array.Exists(DataSource.orderItems, element => element.OrderItemID == _orderItemId))
            throw new Exception("the order Item you try to delete are not exist");

        for (int i = 0; i < DataSource.Config._indexOrdersItems; i++)
        {
            if (DataSource.orderItems[i].OrderItemID == _orderItemId)
            {
                DataSource.orderItems[i] = DataSource.orderItems[--DataSource.Config._indexOrdersItems];
                return;
            }
        }
    }

    /// <summary>
    /// Order item update of a requested product
    /// </summary>
    /// <param name="updateOrderItem"></param>
    /// <exception cref="Exception"></exception>
    public void UpdateOrderItem(OrderItem updateOrderItem)
    {
        if (!Array.Exists(DataSource.orderItems, element => element.OrderItemID == updateOrderItem.OrderItemID))
            throw new Exception("the order items you try to update are not exist");


        for (int i = 0; i < DataSource.Config._indexOrdersItems; i++)
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
    /// <param name="_prodID"></param>
    /// <param name="_orderID"></param>
    /// <returns> returns the corresponding order item </returns>
    /// <exception cref="Exception"></exception>
    public OrderItem FindOrderItem(int _prodID,int _orderID)
    {
        OrderItem _returnOrderItem = new OrderItem();
        for (int i = 0; i < DataSource.Config._indexOrdersItems; i++)
        {
            if (DataSource.orderItems[i].ProductID == _prodID && DataSource.orderItems[i].OrderID == _orderID)
            {
                _returnOrderItem = DataSource.orderItems[i];
                return _returnOrderItem;    
            }
        }
        throw new Exception("the item you search are not found");
    }

    /// <summary>
    /// Looking for all order items of an order
    /// </summary>
    /// <param name="_orderID"></param>
    /// <returns> Returns all order items of the same order ID number </returns>
    /// <exception cref="Exception"></exception>
    public OrderItem[] GetByOrderID(int _orderID)
    {
        if (!Array.Exists(DataSource.orderItems, element => element.OrderID == _orderID))
            throw new Exception("the order item you try to get are not exist");

        int _count = 0;

        _count = DataSource.orderItems.Count(element => element.OrderID == _orderID);

        OrderItem[] orderItemID = new OrderItem[_count];

        for (int i = 0; i < DataSource.Config._indexOrdersItems; i++)
        {
            if (DataSource.orderItems[i].OrderID == _orderID)
            {
                orderItemID[i] = DataSource.orderItems[i];  
            }
        }

        return orderItemID;

    }


}
