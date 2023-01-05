using PL.Order;
using PL.Product;
using System.Windows;

namespace PL.admin;

/// <summary>
/// Interaction logic for AdminWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    /// <summary>
    /// constructor for AdminWindow
    /// </summary>
    public AdminWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// function to open ProductList Window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductList(object sender, RoutedEventArgs e) => new ProductList().Show();

    /// <summary>
    /// function to open OrderList Window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OrderList(object sender, RoutedEventArgs e) => new OrderList().Show();

    /// <summary>
    /// function to open StatisticsOrders Window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StatisticsOrders(object sender, RoutedEventArgs e) => new StatisticsOrders().Show();
}