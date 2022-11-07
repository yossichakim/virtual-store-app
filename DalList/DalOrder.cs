using DO;

namespace Dal;

/// <summary>
/// class for menage order
/// </summary>
public class DalOrder
{
    /// <summary>
    /// Receives an order as a parameter and adds it to the array of orders
    /// </summary>
    /// <param name="addOrder"></param>
    /// <returns> Order ID number </returns>
    /// <exception cref="Exception"> if the array of orders are full </exception>
    public int AddOrder(Order addOrder)
    {
        if (DataSource.indexOrders == DataSource.orders.Length)
            throw new Exception("no more space to add a new orders");

        DataSource.orders[DataSource.indexOrders++] = addOrder;

        return addOrder.OrderID;
    }

    /// <summary>
    /// The function receives an ID number of an order
    /// and checks whether there is a matching order and returns the order
    /// </summary>
    /// <param name="order"></param>
    /// <returns> Returns the requested order </returns>
    /// <exception cref="Exception"> if the order not exist </exception>
    public Order GetOrder(int order)
    {
        if (!Array.Exists(DataSource.orders, element => element.OrderID == order))
            throw new Exception("the order you try to get are not exist");

        Order returnOrder = new Order();

        foreach (var tmpOrder in DataSource.orders)
        {
            if (tmpOrder.OrderID == order)
            {
                returnOrder = tmpOrder;
            }
        }

        return returnOrder;
    }

    /// <summary>
    /// <returns>  Returns the order list </returns>
    /// </summary>
    public Order[] GetAllorders()
    {
        Order[] returnOrdersArr = new Order[DataSource.indexOrders];

        DataSource.orders.CopyTo(returnOrdersArr, 0);

        return returnOrdersArr;
    }

    /// <summary>
    /// Deleting the order from the array by deleting it and replacing it with the last place
    /// </summary>
    /// <param name="orderId"></param>
    /// <exception cref="Exception"> if the order not exist </exception>
    public void RemoveOrder(int orderId)
    {
        if (!Array.Exists(DataSource.orders, element => element.OrderID == orderId))
            throw new Exception("the order you try to delete are not exist");

        for (int i = 0; i < DataSource.indexOrders; i++)
        {
            if (DataSource.orders[i].OrderID == orderId)
            {
                DataSource.orders[i] = DataSource.orders[--DataSource.indexOrders];
                return;
            }
        }
    }

    /// <summary>
    /// Updating an order whose details have changed
    /// </summary>
    /// <param name="updateOrder"></param>
    /// <exception cref="Exception"> if the order not exist </exception>
    public void UpdateOrder(Order updateOrder)
    {
        if (!Array.Exists(DataSource.orders, element => element.OrderID == updateOrder.OrderID))
            throw new Exception("the order you try to update are not exist");

        for (int i = 0; i < DataSource.indexOrders; i++)
        {
            if (updateOrder.OrderID == DataSource.orders[i].OrderID)
            {
                DataSource.orders[i] = updateOrder;

                return;
            }
        }
    }
}