namespace BlImplementation;

internal class Order : BLApi.IOrder
{

    private DalApi.IDal _dal = new Dal.DalList();

    public IEnumerable<BO.OrderForList> GetOrderList()
    {
        List<BO.OrderForList> returnOrderList = new();

        foreach (var item in _dal.Order.GetAll())
        {
            (int amount, double totalPrice) = amountPriceOrder(item);
            BO.OrderForList order = new()
            {
                OrderID = item.OrderID,
                CustomerName = item.CustomerName,
                AmountOfItems = amount,
                TotalPrice = totalPrice,
                Status = getStatus(item)
            };

            returnOrderList.Add(order);
        }

        return returnOrderList;
    }

    public BO.Order GetOrderDetails(int orderID)
    {
        if (orderID <= 0)
        {
            throw new BO.NoValidException("order id");
        }

        try
        {
            DO.Order item = _dal.Order.Get(orderID);
            (_, double totalPrice) = amountPriceOrder(item);
            BO.Order order = new()
            {
                OrderID = item.OrderID,
                CustomerName = item.CustomerName,
                CustomerEmail = item.CustomerEmail,
                CustomerAddress = item.CustomerAddress,
                OrderDate = item.OrderDate,
                ShipDate = item.ShipDate,
                DeliveryDate = item.DeliveryDate,
                ItemsList = returnItemsList(item).ToList(),
                Status = getStatus(item),
                TotalPrice = totalPrice,
            };
            return order;
        }
        catch (DO.NoFoundException ex)
        {
            throw new BO.NoFoundException(ex);
        }
    }

    public BO.Order ShippingUpdate(int orderID)
    {
        try
        {
            DO.Order orderDo = _dal.Order.Get(orderID);
            BO.Order orderBo = GetOrderDetails(orderID);
            if (orderDo.ShipDate == null)
            {
                orderDo.ShipDate = DateTime.Now;
                _dal.Order.Update(orderDo);
                orderBo.ShipDate = orderDo.ShipDate;
                orderBo.Status = getStatus(orderDo);
                return orderBo;
            }
            else
            {
                throw new BO.ErrorUpdateException("shipped");
            }
        }
        catch (DO.NoFoundException ex)
        {
            throw new BO.NoFoundException(ex);
        }
    }

    public BO.Order DeliveryUpdate(int orderID)
    {
        try
        {
            DO.Order orderDo = _dal.Order.Get(orderID);
            BO.Order orderBo = GetOrderDetails(orderID);
            if (orderDo.DeliveryDate == null)
            {
                orderDo.DeliveryDate = DateTime.Now;
                _dal.Order.Update(orderDo);
                orderBo.DeliveryDate = orderDo.DeliveryDate;
                orderBo.Status = getStatus(orderDo);
                return orderBo;
            }
            else
            {
                throw new BO.ErrorUpdateException("delivered");
            }
        }
        catch (DO.NoFoundException ex)
        {
            throw new BO.NoFoundException(ex);
        }
    }

    public BO.OrderTracking OrderTrackingManger(int orderID)
    {
        BO.OrderTracking orderTracking = new();
        try
        {
            DO.Order order = _dal.Order.Get(orderID);

            orderTracking.OrderTrackingID = orderID;
            orderTracking.Status = getStatus(order);
            orderTracking.DateAndStatus = new(){
                Tuple.Create(order.OrderDate, BO.OrderStatus.OrderConfirmed),
                Tuple.Create(order.ShipDate, BO.OrderStatus.OrderSent),
                Tuple.Create(order.DeliveryDate, BO.OrderStatus.OrderProvided)
            };
            return orderTracking;
        }
        catch (DO.NoFoundException ex)
        {
            throw new BO.NoFoundException(ex);
        }
    }

    public void UpdateOrder(int orderID)
    {
        throw new NotImplementedException();
    }

    #region service function

    private BO.OrderStatus getStatus(DO.Order item)
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

    private IEnumerable<BO.OrderItem> returnItemsList(DO.Order item)
    {
        List<BO.OrderItem> items = new();

        foreach (var orderItem in _dal.OrderItem.GetByOrderID(item.OrderID))
        {
            BO.OrderItem temp = new()
            {
                ProductID = orderItem.ProductID,
                Amount = orderItem.Amount,
                ProductPrice = orderItem.Price,
                TotalPrice = orderItem.Amount * orderItem.Price,
                ProductName = returnProductName(orderItem.ProductID)
            };
            items.Add(temp);
        }
        return items;
    }

    private (int, double) amountPriceOrder(DO.Order item)
    {
        List<DO.OrderItem> items = _dal.OrderItem.GetByOrderID(item.OrderID).ToList();

        double totalPrice = items.Sum(element => element.Amount * element.Price);
        int amount = items.Sum(element => element.Amount);

        return (amount, totalPrice);
    }

    private string returnProductName(int productId)
    {
        string productName = string.Empty;  
        foreach (var product in _dal.Product.GetAll())
        {
            if (product.ProductID == productId)
            {
                productName = product.Name;
                break;
            }
        }
       return productName;  
    }

    #endregion service function
}