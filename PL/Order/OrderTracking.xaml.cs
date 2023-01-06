namespace PL.Order;

using System.Windows;

/// <summary>
/// Interaction logic for OrderTracking.xaml
/// </summary>
public partial class OrderTracking : Window
{
    /// <summary>
    /// Access to the logical layer
    /// </summary>
    private BLApi.IBl? _bl = BLApi.Factory.Get();

    /// <summary>
    /// View order tracking link
    /// </summary>
    public static readonly DependencyProperty OrderTrackingDep = DependencyProperty.Register(nameof(OrderTrackingProp), typeof(BO.OrderTracking), typeof(OrderTracking));

    /// <summary>
    /// Object for order tracking
    /// </summary>
    public BO.OrderTracking? OrderTrackingProp { get => (BO.OrderTracking?)GetValue(OrderTrackingDep); set => SetValue(OrderTrackingDep, value); }

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="orderId"></param>
    public OrderTracking(int orderId)
    {
        InitializeComponent();
        OrderTrackingProp = _bl!.Order.OrderTrackingManger(orderId)!;
    }

    /// <summary>
    /// Access for viewing order details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OrderDetails(object sender, RoutedEventArgs e)
    {
        bool view = true;
        new Order(OrderTrackingProp!.OrderID, view).Show();
    }
}