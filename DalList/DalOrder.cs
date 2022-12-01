using DalApi;
using DO;

namespace Dal;

/// <summary>
/// class for menage order
/// </summary>
internal class DalOrder : IOrder
{
    /// <summary>
    /// Receives an order as a parameter and adds it to the array of orders
    /// </summary>
    /// <param name="addOrder"></param>
    /// <returns> Order ID number </returns>
    /// <exception cref="AddException"> if the array of orders are full </exception>
    public int Add(Order addOrder)
    {
        if (DataSource.orders.Exists(element => element?.OrderID == addOrder.OrderID))
            throw new AddException("order");
        addOrder.OrderID = DataSource.getOrderID;
        DataSource.orders.Add(addOrder);

        return addOrder.OrderID;
    }

    /// <summary>
    /// The function receives an ID number of an order
    /// and checks whether there is a matching order and returns the order
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns> Returns the requested order </returns>
    /// <exception cref="NoFoundException"> if the order not exist </exception>
    public Order Get(int orderID)
    {
        if (!DataSource.orders.Exists(element => element?.OrderID == orderID))
            throw new NoFoundException("order");

        Order ?returnOrder = new Order();

        foreach (var tmpOrder in DataSource.orders)
        {
            if (tmpOrder?.OrderID == orderID)
            {
                returnOrder = tmpOrder;
            }
        }

        return (Order)returnOrder;
    }

    /// <summary>
    /// <returns>  Returns the order list </returns>
    /// </summary>
    public IEnumerable<Order?> GetAll(Func <Order?, bool>? func = null)
    {
        return DataSource.orders.Select(item => item);
    }

    /// <summary>
    /// Deleting the order from the array by deleting it and replacing it with the last place
    /// </summary>
    /// <param name="orderID"></param>
    /// <exception cref="NoFoundException"> if the order not exist </exception>
    public void Delete(int orderID)
    {
        if (!DataSource.orders.Exists(element => element?.OrderID == orderID))
            throw new NoFoundException("order");

        DataSource.orders.RemoveAll(element => element?.OrderID == orderID);

        //return;
    }

    /// <summary>
    /// Updating an order whose details have changed
    /// </summary>
    /// <param name="updateOrder"></param>
    /// <exception cref="NoFoundException"> if the order not exist </exception>
    public void Update(Order updateOrder)
    {
        if (!DataSource.orders.Exists(element => element?.OrderID == updateOrder.OrderID))
            throw new NoFoundException("order");

        //int index = DataSource.orders.FindIndex(updateOrder);
        //DataSource.orders.ElementAt(0);

        int index = 0;
        foreach (var item in DataSource.orders)
        {
            if (updateOrder.OrderID == item?.OrderID)
            {
                DataSource.orders[index] = updateOrder;
                return;
            }
            index++;
        }
    }
}