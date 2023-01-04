using PL.Order;
using PL.Product;
using System.Windows;

namespace PL.admin;

/// <summary>
/// Interaction logic for AdminWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    public AdminWindow()
    {
        InitializeComponent();
    }

    private void ProductList(object sender, RoutedEventArgs e) => new ProductList().Show();

    private void OrderList(object sender, RoutedEventArgs e) => new OrderList().Show();

    private void StatisticsOrders(object sender, RoutedEventArgs e) => new StatisticsOrders().Show();
}