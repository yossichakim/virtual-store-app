namespace PL;
using PL.admin;
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
    private void CustomerAccsess(object sender, RoutedEventArgs e) => new Customer.CustomerWindow().Show();
}