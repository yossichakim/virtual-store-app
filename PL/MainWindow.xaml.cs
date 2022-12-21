using PL.admin;
using System.Windows;

namespace PL;

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
    /// Admin access to product list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AdminAccsess(object sender, RoutedEventArgs e) => new AdminWindow().Show();

    private void NewOrder(object sender, RoutedEventArgs e)
    {
    }

    private void OrderTracking(object sender, RoutedEventArgs e)
    {
    }
}