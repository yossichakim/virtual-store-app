namespace BlImplementation;

internal class Order : BLApi.IOrder
{
    private DalApi.IDal Dal = new Dal.DalList();

    private BO.OrderStatus GetStatus(DO.Order item)
    {
        BO.OrderStatus status = new();

        if (item.DeliveryDate < DateTime.Now)
        {
            status = BO.OrderStatus.OrderProvided;
        }
        else if (item.ShipDate < DateTime.Now)
        {
            status = BO.OrderStatus.OrderSent;
        }
        else if (item.OrderDate < DateTime.Now)
        {
            status = BO.OrderStatus.OrderConfirmed;
        }

        return status;
    }

    private IEnumerable<BO.OrderItem> ReturnItemsList(DO.Order item)
    {
        List<BO.OrderItem> items = new();

        foreach (var orderItem in Dal.OrderItem.GetByOrderID(item.OrderID))
        {
            BO.OrderItem temp = new()
            {
                OrderItemID = orderItem.OrderItemID,
                ProductID = orderItem.ProductID,
                Amount = orderItem.Amount,
                ProductPrice = orderItem.Price,
                TotalPrice = orderItem.Amount * orderItem.Price
            };
            items.Add(temp);
        }
        return items;
    }

    private (int, double) AmountPriceOrder(DO.Order item)
    {
        List<DO.OrderItem> items = new();
        items = Dal.OrderItem.GetByOrderID(item.OrderID).ToList();

        double totalPrice = items.Sum(element => element.Amount * element.Price);
        int amount = items.Sum(element => element.Amount);

        return (amount, totalPrice);
    }

    public IEnumerable<BO.OrderForList> GetOrderList()
    {
        List<BO.OrderForList> returnOrderList = new();

        foreach (var item in Dal.Order.GetAll())
        {
            (int amount, double totalPrice) = AmountPriceOrder(item);
            BO.OrderForList order = new()
            {
                OrderID = item.OrderID,
                CustomerName = item.CustomerName,
                AmountOfItems = amount,
                TotalPrice = totalPrice,
                Status = GetStatus(item)
            };

            returnOrderList.Add(order);
        }

        return returnOrderList;
    }

    public BO.Order GetOrderDetails(int orderID)
    {
        if (orderID > 0)
        {
            foreach (var item in Dal.Order.GetAll())
            {
                if (orderID == item.OrderID)
                {
                    (_, double totalPrice) = AmountPriceOrder(item);
                    BO.Order order = new()
                    {
                        OrderID = item.OrderID,
                        CustomerName = item.CustomerName,
                        CustomerEmail = item.CustomerEmail,
                        CustomerAddress = item.CustomerAddress,
                        OrderDate = item.OrderDate,
                        ShipDate = item.ShipDate,
                        DeliveryDate = item.DeliveryDate,
                        ItemsList = ReturnItemsList(item).ToList(),
                        Status = GetStatus(item),
                        TotalPrice = totalPrice,
                    };
                    return order;
                }
            }
        }

        throw new Exception("error");
    }

    public BO.Order ShippingUpdate(int orderID)
    {
        try
        {
            DO.Order order1 = Dal.Order.Get(orderID);
            BO.Order order = GetOrderDetails(orderID);
            if (order1.ShipDate == null)
            {
                order1.ShipDate = DateTime.Now;
                Dal.Order.Update(order1);
                order.ShipDate = order1.ShipDate;
                order.Status = GetStatus(order1);
                return order;
            }
            else
            {
                throw new Exception("erorr");
            }
        }
        catch (Exception)
        {
            throw new Exception("ujwjijxi");
        }
    }

    public BO.Order DeliveryUpdate(int orderID)
    {
        try
        {
            DO.Order order1 = Dal.Order.Get(orderID);
            BO.Order order = GetOrderDetails(orderID);
            if (order1.DeliveryDate == null)
            {
                order1.DeliveryDate = DateTime.Now;
                Dal.Order.Update(order1);
                order.ShipDate = order1.DeliveryDate;
                order.Status = GetStatus(order1);
                return order;
            }
            else
            {
                throw new Exception("erorr");
            }
        }
        catch (Exception)
        {
            throw new Exception("error");
        }
    }

    public BO.OrderTracking OrderTrackingManger(int orderID)
    {
        BO.OrderTracking orderTracking = new();
        try
        {
            DO.Order order = Dal.Order.Get(orderID);

            orderTracking.OrderTrackingID = orderID;
            orderTracking.Status = GetStatus(order);
            orderTracking.DateAndStatus = new(){
                Tuple.Create(order.OrderDate, BO.OrderStatus.OrderConfirmed),
                Tuple.Create(order.ShipDate, BO.OrderStatus.OrderSent),
                Tuple.Create(order.DeliveryDate, BO.OrderStatus.OrderProvided)
            };
            return orderTracking;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void UpdateOrder(int orderID)
    {
        throw new NotImplementedException();
    }
}