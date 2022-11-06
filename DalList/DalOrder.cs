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
    /// <param name="_addOrder"></param>
    /// <returns> Order ID number </returns>
    /// <exception cref="Exception"> If no more space to add a new orders </exception>
    public int AddOrder(Order _addOrder)
    {

        if (DataSource.Config._indexOrders == DataSource.orders.Length)
            throw new Exception("no more space to add a new orders");


        DataSource.orders[DataSource.Config._indexOrders++] = _addOrder;

        return _addOrder.OrderID;

    }

    /// <summary>
    /// The function receives an ID number of an order
    /// and checks whether there is a matching order and returns the order
    /// </summary>
    /// <param name="_order"></param>
    /// <returns> Returns the requested order </returns>
    /// <exception cref="Exception"> If the order not exist </exception>
    public Order GetOrder(int _order)
    {
        if (!Array.Exists(DataSource.orders, _element => _element.OrderID == _order))
            throw new Exception("the order you try to get are not exist");

        Order _returnOrder = new Order();

        foreach (var _tmpOrder in DataSource.orders)
        {
            if (_tmpOrder.OrderID == _order)
            {
                _returnOrder = _tmpOrder;
            }
        }

        return _returnOrder;
    }


  
    /// <returns>  Returns the order list </returns>
    public Order[] GetAllorders()
    {
        Order[] _orders = new Order[DataSource.Config._indexOrders];

        DataSource.orders.CopyTo(_orders, 0);

        return _orders;
    }

    /// <summary>
    /// Deleting the order from the array by deleting it and replacing it with the last place
    /// </summary>
    /// <param name="_orderId"></param>
    /// <exception cref="Exception"> If the order not exist </exception>
    public void RemoveOrder(int _orderId)
    {
        if (!Array.Exists(DataSource.orders, _element => _element.OrderID == _orderId))
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
    /// <param name="_updateOrder"></param>
    /// <exception cref="Exception"> If the order not exist </exception>
    public void UpdateOrder(Order _updateOrder)
    {
        if (!Array.Exists(DataSource.orders, _element => _element.OrderID == _updateOrder.OrderID))
            throw new Exception("the order you try to update are not exist");


        for (int i = 0; i < DataSource.Config._indexOrders; i++)
        {
            if (_updateOrder.OrderID == DataSource.orders[i].OrderID)
            {
                DataSource.orders[i] = _updateOrder;

                return;
            }
        }
    }

}
