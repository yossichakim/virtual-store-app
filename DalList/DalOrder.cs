using DO;

namespace Dal;

public class DalOrder
{
    /// <summary>
    /// Receives an order as a parameter and adds it to the array of orders
    /// </summary>
    /// <param name="addOrder"></param>
    /// <returns> Order ID number </returns>
    /// <exception cref="Exception"></exception>
    public int AddOrder(Order addOrder)
    {

        if (DataSource.Config._indexOrders == DataSource.orders.Length)
            throw new Exception("no more space to add a new orders");


        DataSource.orders[DataSource.Config._indexOrders++] = addOrder;

        return addOrder.OrderID;

    }

    /// <summary>
    /// The function receives an ID number of an order
    /// and checks whether there is a matching order and returns the order
    /// </summary>
    /// <param name="_order"></param>
    /// <returns> Returns the requested order </returns>
    /// <exception cref="Exception"></exception>
    public Order GetOrder(int _order)
    {
        if (!Array.Exists(DataSource.orders, element => element.OrderID == _order))
            throw new Exception("the order you try to get are not exist");

        Order returnOrder = new Order();

        foreach (var tmpOrder in DataSource.orders)
        {
            if (tmpOrder.OrderID == _order)
            {
                returnOrder = tmpOrder;
            }
        }

        return returnOrder;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns>  Returns the order list </returns>
    public Order[] GetAllorders()
    {
        Order[] orders = new Order[DataSource.Config._indexOrders];

        DataSource.orders.CopyTo(orders, 0);

        return orders;
    }

    /// <summary>
    /// Deleting the order from the array by deleting it and replacing it with the last place
    /// </summary>
    /// <param name="_orderId"></param>
    /// <exception cref="Exception"></exception>
    public void RemoveOrder(int _orderId)
    {
        if (!Array.Exists(DataSource.orders, element => element.OrderID == _orderId))
            throw new Exception("the order you try to delete are not exist");

        for (int i = 0; i < DataSource.Config._indexOrders; i++)
        {
            if (DataSource.orders[i].OrderID == _orderId)
            {
                DataSource.orders[i] = DataSource.orders[--DataSource.Config._indexOrders];
                return;
            }
        }
    }

    /// <summary>
    /// Updating an order whose details have changed
    /// </summary>
    /// <param name="updateOrder"></param>
    /// <exception cref="Exception"></exception>
    public void UpdateOrder(Order updateOrder)
    {
        if (!Array.Exists(DataSource.orders, element => element.OrderID == updateOrder.OrderID))
            throw new Exception("the order you try to update are not exist");


        for (int i = 0; i < DataSource.Config._indexOrders; i++)
        {
            if (updateOrder.OrderID == DataSource.orders[i].OrderID)
            {
                DataSource.orders[i] = updateOrder;
                return;
            }
        }
    }

}
