using DO;

namespace Dal;

public class DalOrder
{
    public int AddOrder(Order addOrder)
    {

        if (DataSource.Config._indexOrders == DataSource.orders.Length)
            throw new Exception("no more space to add a new orders");


        DataSource.orders[DataSource.Config._indexOrders++] = addOrder;   

        return addOrder.OrderID;

    }

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


    public Order[] GetAllorders()
    {
        Order[] orders = new Order[DataSource.Config._indexOrders];

        DataSource.orders.CopyTo(orders, 0);

        return orders;
    }

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
