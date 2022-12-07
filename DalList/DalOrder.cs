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
            throw new AddException("ORDER");

        //initialize Running ID number
        addOrder.OrderID = DataSource.getOrderID;
        DataSource.orders.Add(addOrder);

        return addOrder.OrderID;
    }

    /// <summary>
    /// Deleting the order from the array by deleting it and replacing it with the last place
    /// </summary>
    /// <param name="orderID"></param>
    /// <exception cref="NoFoundException"> if the order not exist </exception>
    public void Delete(int orderID)
    {
        if (!DataSource.orders.Exists(element => element?.OrderID == orderID))
            throw new NoFoundException("ORDER");

        DataSource.orders.RemoveAll(element => element?.OrderID == orderID);
    }

    /// <summary>
    /// Updating an order whose details have changed
    /// </summary>
    /// <param name="updateOrder"></param>
    /// <exception cref="NoFoundException"> if the order not exist </exception>
    public void Update(Order updateOrder)
    {
        Delete(updateOrder.OrderID);
        DataSource.orders.Add(updateOrder);
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
        return Get(element => orderID == element?.OrderID);
    }

    /// <summary>
    /// The function receives an condition of an order
    /// and checks whether there is a matching order and returns the order
    /// </summary>
    /// <param name="func"></param>
    /// <returns> Returns the requested order </returns>
    /// <exception cref="NoFoundException"></exception>
    public Order Get(Func<Order?, bool>? func)
    {
        if (DataSource.orders.FirstOrDefault(func!) is Order order)
            return order;

        throw new NoFoundException("ORDER");
    }

    /// <summary>
    /// <returns>  Returns the order list in condition </returns>
    /// </summary>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func = null)
     => func is null ? DataSource.orders.Select(item => item) :
       DataSource.orders.Where(func);
}