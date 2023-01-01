using System;
using System.Windows;
namespace PL.Order;

/// <summary>
/// Interaction logic for ProductItem.xaml
/// </summary>
public partial class ProductItem : Window
{
    private BLApi.IBl? _bl;
    private BO.Cart? _cart;
    private BO.ProductItem? productItem = new();
    private NewOrder newOrder;
    public ProductItem(BLApi.IBl? bl, BO.Cart cart, NewOrder sender)
    {
        InitializeComponent();
        _bl = bl;
        _cart = cart;
        newOrder = sender;
        Catgory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        Catgory.SelectedItem = productItem.Categoty;
    }
    public ProductItem(BLApi.IBl? bl, int ViewProductID, BO.Cart cart, NewOrder sender)
        : this(bl,cart,sender)
    {
        productItem = _bl?.Product.GetProductCostumer(ViewProductID, _cart)!;
        DataContext = productItem;
        AmountInCart.IsEnabled = false;
        if (productItem.InStock == false)
            AddToCart.Visibility = Visibility.Hidden;
        UpdateCart.Visibility = Visibility.Hidden;
        AmountInCart.IsEnabled = false;
    }
    public ProductItem(BLApi.IBl? bl, int ViewProductID, BO.Cart cart, string updateCart, NewOrder sender)
        : this(bl, cart, sender)
    {
        productItem = _bl?.Product.GetProductCostumer(ViewProductID, _cart)!;
        DataContext = productItem;
        AddToCart.Visibility = Visibility.Hidden;
    }

    private void AddToCart_Click(object sender, RoutedEventArgs e)
    {

        try
        {
            _cart = _bl?.Cart.AddProductToCart(_cart!, (int)productItem?.ProductID!);
            this.Close();
            newOrder.productItemLists = _bl?.Product.GetProductListCostumer(_cart!)!;
            new PL.Cart.Cart(_cart!, newOrder).Show();
        } catch (BO.NoFoundException ex)
        {
            MessageBox.Show(ex.Message + ex.InnerException, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        } catch (BO.NoValidException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void UpdateCart_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            _cart = _bl?.Cart.UpdateAmount(_cart!, (int)productItem?.ProductID!, int.Parse(AmountInCart.Text));
            this.Close();
            newOrder.productItemLists = _bl?.Product.GetProductListCostumer(_cart!)!;
            new PL.Cart.Cart(_cart!, newOrder).Show();
        } 
        catch (BO.NoValidException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        } 
        catch (BO.NoFoundException ex)
        {
            MessageBox.Show(ex.Message + ex.InnerException, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}


