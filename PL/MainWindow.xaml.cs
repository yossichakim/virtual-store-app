using BLApi;
using BlImplementation;
using PL.Product;
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
    private IBl _bl = new Bl();

    /// <summary>
    /// constructor
    /// </summary>
    public MainWindow() => InitializeComponent();

    /// <summary>
    /// Admin access to product list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AdminAccsess(object sender, RoutedEventArgs e) => new ProductList().Show();
}