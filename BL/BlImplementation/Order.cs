namespace BlImplementation;

/// <summary>
/// Order interface implementation class
/// </summary>
internal class Order : BLApi.IOrder
{
    private DalApi.IDal? _dal = DalApi.Factory.Get();

    /// <summary>
    /// The function returns a list of all orders
    /// </summary>
    /// <returns>returns a list of all orders</returns>
    public IEnumerable<BO.OrderForList?> GetOrderList()
    {
        return from order in _dal?.Order.GetAll()
               select new BO.OrderForList
               {
                   OrderID = (int)order?.OrderID!,
                   CustomerName = order?.CustomerName,
                   AmountOfItems = (int)amountPriceOrder((DO.Order)order!).Item1!,
                   TotalPrice = (double)amountPriceOrder((DO.Order)order!).Item2!,
                   Status = getStatus((DO.Order)order!)
               };
    }

    /// <summary>
    /// The function receives an order ID and returns the order
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns>returns the order</returns>
    public BO.Order GetOrderDetails(int orderID)
    {
        if (orderID <= 0)
        {
            throw new BO.NoValidException("order id");
        }

        try
        {
            DO.Order item = (DO.Order)_dal?.Order.Get(orderID)!;
            (_, double? totalPrice) = amountPriceOrder(item);
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
                TotalPrice = (double)totalPrice!,
            };
            return order;
        }
        catch (DO.NoFoundException ex)
        {
            throw new BO.NoFoundException(ex);
        }
    }

    /// <summary>
    /// The function receives an order ID and updates a shipping date
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns>Returns an order with an updated shipping date</returns>
    public BO.Order ShippingUpdate(int orderID)
    {
        try
        {
            DO.Order orderDo = (DO.Order)_dal?.Order.Get(orderID)!;
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

    /// <summary>
    /// The function receives an order ID and updates a date of delivery
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns>Returns an order with an updated delivery date</returns>
    /// <exception cref="BO.ErrorUpdateException"></exception>
    /// <exception cref="BO.NoFoundException"></exception>
    public BO.Order DeliveryUpdate(int orderID)
    {
        try
        {
            DO.Order orderDo = (DO.Order)_dal?.Order.Get(orderID)!;
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

    /// <summary>
    /// The function receives an order ID and displays the order status
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    /// <exception cref="BO.NoFoundException"></exception>
    public BO.OrderTracking OrderTrackingManger(int orderID)
    {
        BO.OrderTracking orderTracking = new();
        try
        {
            DO.Order order = (DO.Order)_dal?.Order.Get(orderID)!;

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

    #region service function

    /// <summary>
    /// Gets an order data entity and returns its status
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Gets an order data entity and returns a list of order items of type logical entity
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private IEnumerable<BO.OrderItem?> returnItemsList(DO.Order item)
    {
        return from orderItem in _dal?.OrderItem.GetAll(orderItem => orderItem?.OrderID == item.OrderID)!
               select new BO.OrderItem
               {
                   ProductID = (int)orderItem?.ProductID!,
                   Amount = (int)orderItem?.Amount!,
                   ProductPrice = (double)orderItem?.Price!,
                   TotalPrice = (int)orderItem?.Amount! * (double)orderItem?.Price!,
                   ProductName = _dal?.Product.Get(product => product?.ProductID == orderItem?.ProductID).Name
               };
    }

    /// <summary>
    /// Gets an order data entity and returns quantity of products and total price of products
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private (int?, double?) amountPriceOrder(DO.Order item)
    {
        List<DO.OrderItem?> items = _dal?.OrderItem.GetAll(element => item.OrderID == element?.OrderID).ToList()!;

        double? totalPrice = items.Sum(element => element?.Amount * element?.Price);
        int? amount = items.Count();

        return (amount, totalPrice);
    }

    #endregion service function
}