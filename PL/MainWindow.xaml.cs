namespace PL;
using PL.admin;
using PL.Customer;
using System.Windows;



/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Access for the logical layer
    /// </summary>
    private BLApi.IBl? _bl = BLApi.Factory.Get();

    /// <summary>
    /// constructor
    /// </summary>
    public MainWindow() => InitializeComponent();

    /// <summary>
    /// Admin access
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AdminAccsess(object sender, RoutedEventArgs e) => new AdminWindow().Show();

    /// <summary>
    /// customer access
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CustomerAccsess(object sender, RoutedEventArgs e) => new CustomerWindow().Show();

    /// <summary>
    /// simulator access
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SimulatorForOrders(object sender, RoutedEventArgs e)
    {
        if (_bl?.Order.GetOldOrderId() is null)
        {
            MessageBox.Show("ALL ORDERS IS DELIVERED", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        new SimulatorWindow().Show();
    }
}