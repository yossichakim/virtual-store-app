namespace PL.Order;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

/// <summary>
/// Interaction logic for NewOrder.xaml
/// </summary>
public partial class NewOrder : Window
{
    private BO.Cart? _cart;

    /// <summary>
    /// Access to the logical layer
    /// </summary>
    private static BLApi.IBl? s_bl = BLApi.Factory.Get();

    public static readonly DependencyProperty CategoryProp = DependencyProperty.Register(nameof(Category), typeof(BO.Category?), typeof(NewOrder));
    public BO.Category? Category { get => (BO.Category?)GetValue(CategoryProp); set => SetValue(CategoryProp, value); }
    public static BO.Category[] Categories { get; } = (BO.Category[])Enum.GetValues(typeof(BO.Category));

    /// <summary>
    /// Saving the list of products
    /// </summary>
    private IEnumerable<IGrouping<BO.Category?, BO.ProductItem>> groupings;

    public static readonly DependencyProperty ListPropProductItem = DependencyProperty.Register(nameof(ProductItemLists), typeof(IEnumerable<BO.ProductItem?>), typeof(NewOrder));
    public IEnumerable<BO.ProductItem?> ProductItemLists { get => (IEnumerable<BO.ProductItem?>)GetValue(ListPropProductItem); set => SetValue(ListPropProductItem, value); }

    public NewOrder()
    {
        InitializeComponent();
        Category = null;
        _cart = new();
        ProductItemLists = s_bl!.Product.GetProductListCostumer(_cart);
        groupings = from item in ProductItemLists
                    group item by item.Categoty into x
                    select x;
    }

    private void updateProductItems()
    {
        if (Category == null)
            ProductItemLists = s_bl!.Product.GetProductListCostumer(_cart!);
        else
            ProductItemLists = s_bl!.Product.GetProductListCostumer(_cart!, item => item!.Categoty == Category);
    }

    private void FilterCatgory_SelectionChanged(object sender, SelectionChangedEventArgs e) => updateProductItems();

    private void ProductItemListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (IsMouseCaptureWithin)
            new ProductItem(_cart!, ((BO.ProductItem)ProductItemListview.SelectedItem).ProductID, updateProductItems).Show();
    }

    private void ShowCart(object sender, RoutedEventArgs e)
    {
        new Cart.Cart(_cart!, updateProductItems).Show();
    }

    private void AllCategory(object sender, RoutedEventArgs e)
    {
        Category = null;
        updateProductItems();
    }
}