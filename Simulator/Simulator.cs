using BLApi;
using BO;

namespace Simulator;

/// <summary>
/// class for simulator for orders
/// </summary>
public static class Simulator
{
    /// <summary>
    /// access for logical layer
    /// </summary>
    static readonly IBl? bl = Factory.Get();

    /// <summary>
    /// random field
    /// </summary>
    public static readonly Random random = new Random();

    /// <summary>
    /// flag for stooping the thread.
    /// </summary>
    private volatile static bool flag;

    /// <summary>
    /// entity order
    /// </summary>
    private static Order? order;

    /// <summary>
    /// action for stop simulator
    /// </summary>
    private static event Action? stopSim;

    public static event Action? StopSim
    {
        add => stopSim += value;
        remove => stopSim -= value;
    }

    /// <summary>
    /// action for update order
    /// </summary>
    private static event Action<Order, OrderStatus?, DateTime, int>? updateOrder;

    public static event Action<Order, OrderStatus?, DateTime, int>? UpdateOrder
    {
        add => updateOrder += value;
        remove => updateOrder -= value;
    }

    /// <summary>
    /// action for update order complete
    /// </summary>
    private static event Action<OrderStatus?>? updateComplete;

    public static event Action<OrderStatus?>? UpdateComplete
    {
        add => updateComplete += value;
        remove => updateComplete -= value;
    }


    private static void invokeList(Delegate[] delegates, params object[] values)
    {
        foreach (var @delegate in delegates)
        {
            @delegate?.DynamicInvoke(values);
        }
    }

    /// <summary>
    /// start the thread simulation
    /// </summary>

    public static void StartSimulation()
    {
        flag = true;

        Delegate[] updateOrderDelegate = updateOrder?.GetInvocationList()!;
        Delegate[] updateCompleteDelegate = updateComplete?.GetInvocationList()!;

        new Thread(() =>
        {
            while (flag)
            {
                int? id = bl!.Order.GetOldOrderId();

                if (id is not null)
                {
                    order = bl!.Order.GetOrderDetails((int)id);
                    int treatTime = random.Next(3, 11);

                    invokeList(updateOrderDelegate, order, (order.Status + 1)!, DateTime.Now, treatTime);

                    Thread.Sleep(treatTime * 1000);

                    if (order.Status == OrderStatus.OrderConfirmed)
                    {
                        bl!.Order.ShippingUpdate(order.OrderID);
                        invokeList(updateCompleteDelegate, OrderStatus.OrderSent);
                    }
                    else
                    {
                        bl!.Order.DeliveryUpdate(order.OrderID);
                        invokeList(updateCompleteDelegate, OrderStatus.OrderProvided);
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                }

                Thread.Sleep(1000);
            }
        }).Start();
    }

    /// <summary>
    /// stop the thread of simulation  
    /// </summary>
    public static void StopSimulation()
    {
        invokeList(stopSim?.GetInvocationList()!);
        flag = false;
    }
}