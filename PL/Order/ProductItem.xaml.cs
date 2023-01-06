namespace PL.Order;

using System.Windows;

/// <summary>
/// Interaction logic for ProductItem.xaml
/// </summary>
public partial class ProductItem : Window
{
    /// <summary>
    /// Access to the logical layer
    /// </summary>
    private static BLApi.IBl? s_bl = BLApi.Factory.Get();

    /// <summary>
    /// Object for shopping cart
    /// </summary>
    private BO.Cart? _cart;

    /// <summary>
    /// Product item link for display if updated
    /// </summary>
    public static readonly DependencyProperty ProductItemDep = DependencyProperty.Register(nameof(ProductItemProp),
                                                                                   typeof(BO.ProductItem),
                                                                                  typeof(ProductItem));

    /// <summary>
    /// object for a product item
    /// </summary>
    public BO.ProductItem? ProductItemProp { get => (BO.ProductItem?)GetValue(ProductItemDep); set => SetValue(ProductItemDep, value); }

    /// <summary>
    /// If a product is updated, the list is also updated
    /// </summary>
    private event Action _productItemChanged;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="ViewProductID"></param>
    /// <param name="productItemChanged"></param>
    /// <param name="flag"></param>
    public ProductItem(BO.Cart cart, int ViewProductID, Action productItemChanged, bool flag = true)
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

    /// <summary>
    /// Adding a product to the cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// Update a product in cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateCart_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(AmountInCart.Text, out int n))
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