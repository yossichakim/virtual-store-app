namespace PL.Order;
using System.Windows;

/// <summary>
/// Interaction logic for ProductItem.xaml
/// </summary>
public partial class ProductItem : Window
{
    private static BLApi.IBl? s_bl = BLApi.Factory.Get();

    private BO.Cart? _cart;

    public static readonly DependencyProperty ProductItemDep = DependencyProperty.Register(nameof(ProductItemProp),
                                                                                   typeof(BO.ProductItem),
                                                                                  typeof(ProductItem));
    public BO.ProductItem? ProductItemProp { get => (BO.ProductItem?)GetValue(ProductItemDep); set => SetValue(ProductItemDep, value); }

    private event Action _productItemChanged;
    public ProductItem(BO.Cart cart,int ViewProductID, Action productItemChanged, bool flag = true)
    {
        InitializeComponent();
        _productItemChanged = productItemChanged;
        _cart = cart;
        ProductItemProp = s_bl?.Product.GetProductCostumer(ViewProductID, _cart)!;
        if (flag)
        {
            if (ProductItemProp.InStock == false)
                AddToCart.Visibility = Visibility.Hidden;
            UpdateCart.Visibility = Visibility.Hidden;
            AmountInCart.IsEnabled = false;
        }
        else
            AddToCart.Visibility = Visibility.Hidden;
    }


    private void AddToCart_Click(object sender, RoutedEventArgs e)
    {

        try
        {
            _cart = s_bl?.Cart.AddProductToCart(_cart!, (int)ProductItemProp?.ProductID!);
            this.Close();
            _productItemChanged.Invoke();
        } 
        catch (BO.NoFoundException ex)
        {
            MessageBox.Show(ex.Message + ex.InnerException, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (BO.NoValidException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void UpdateCart_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse( AmountInCart.Text, out int n))
            MessageBox.Show("ENTER A VALID AMOUNT", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

        try
        {
            _cart = s_bl?.Cart.UpdateAmount(_cart!, (int)ProductItemProp?.ProductID!, int.Parse(AmountInCart.Text));
            this.Close();
            _productItemChanged.Invoke();
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