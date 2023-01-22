using BLApi;
using BO;

namespace Simulator;

public static class Simulator
{
    static readonly IBl? bl = Factory.Get();

    public static readonly Random random = new Random(DateTime.Now.Millisecond);

    private volatile static bool run;

    private static Order? order;

    private static event Action? stopSimulator;

    private static event Action<Order, OrderStatus?, DateTime, int>? updateOrder;

    private static event Action<OrderStatus?>? UpdateComplete;

    public static void StartSimulation()
    {
        new Thread(() =>
        {
            run = true;

            while (run)
            {

                int? id = bl!.Order.GetOldOrderId();

                if (id != null)
                {

                    order = bl!.Order.GetOrderDetails((int)id);

                    int treatTime = random.Next(3, 11);

                    updateOrder?.Invoke(order, order.Status + 1, DateTime.Now, treatTime);

                    Thread.Sleep(treatTime * 1000);

                    if (order.Status == OrderStatus.OrderConfirmed)
                    {
                        bl!.Order.ShippingUpdate(order.OrderID);
                        UpdateComplete?.Invoke(OrderStatus.OrderSent);
                    }
                    else
                    {
                        bl!.Order.DeliveryUpdate(order.OrderID);
                        UpdateComplete?.Invoke(OrderStatus.OrderProvided);
                    }
                }
                else
                    StopSimulation();

                Thread.Sleep(1000);
            }
        }).Start();
    }

    public static void StopSimulation()
    {
        stopSimulator?.Invoke();
        run = false;
    }

    public static void RegisterToStop(Action action) => stopSimulator += action;
    public static void DeRegisterToStop(Action action) => stopSimulator -= action;

    public static void RegisterToComplete(Action<OrderStatus?> action) => UpdateComplete += action;
    public static void DeRegisterToComplete(Action<OrderStatus?> action) => UpdateComplete -= action;

    public static void RegisterToUpdtes(Action<Order, OrderStatus?, DateTime, int> action) => updateOrder += action;
    public static void DeRegisterToUpdtes(Action<Order, OrderStatus?, DateTime, int> action) => updateOrder -= action;

}

