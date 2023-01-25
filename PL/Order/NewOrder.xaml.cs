namespace PL.Order;

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

/// <summary>
/// Interaction logic for NewOrder.xaml
/// </summary>
public partial class NewOrder : Window
{
    /// <summary>
    /// Object for shopping cart
    /// </summary>
    private BO.Cart? _cart;

    /// <summary>
    /// Access to the logical layer
    /// </summary>
    private static BLApi.IBl? s_bl = BLApi.Factory.Get();

    /// <summary>
    /// Dependency Property for Category
    /// </summary>
    public static readonly DependencyProperty CategoryProp = DependencyProperty.Register(nameof(Category), typeof(BO.Category?), typeof(NewOrder));

    /// <summary>
    ///  Category
    /// </summary>
    public BO.Category? Category { get => (BO.Category?)GetValue(CategoryProp); set => SetValue(CategoryProp, value); }

    /// <summary>
    /// for present enum of Categories
    /// </summary>
    public static BO.Category[] Categories { get; } = (BO.Category[])Enum.GetValues(typeof(BO.Category));

    /// <summary>
    /// Dependency Property for update product item list
    /// </summary>
    public static readonly DependencyProperty ListPropProductItem = DependencyProperty.Register(nameof(ProductItemLists), typeof(IEnumerable<BO.ProductItem?>), typeof(NewOrder));

    /// <summary>
    /// Object for product item list
    /// </summary>
    public IEnumerable<BO.ProductItem?> ProductItemLists { get => (IEnumerable<BO.ProductItem?>)GetValue(ListPropProductItem); set => SetValue(ListPropProductItem, value); }

    /// <summary>
    /// for present by grouping
    /// </summary>
    public ICollectionView ProductItemsCollectionView { set; get; }

    /// <summary>
    /// Dependency Property for grouping
    /// </summary>
    private PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");

    /// <summary>
    /// constructor
    /// </summary>
    public NewOrder()
    {
        InitializeComponent();
        Category = null;
        _cart = new();

        ProductItemLists = s_bl!.Product.GetProductListCostumer(_cart);
        ProductItemsCollectionView = CollectionViewSource.GetDefaultView(ProductItemLists);
    }

    /// <summary>
    /// update product item
    /// </summary>
    private void updateProductItems()
    {
        if (Category == null)
            ProductItemLists = s_bl!.Product.GetProductListCostumer(_cart!);
        else
            ProductItemLists = s_bl!.Product.GetProductListCostumer(_cart!, item => item!.Category == Category);

        ProductItemsCollectionView = CollectionViewSource.GetDefaultView(ProductItemLists);

        if (CheckBoxGrop.IsChecked == true)
            ProductItemsCollectionView.GroupDescriptions.Add(groupDescription);
        else
            ProductItemsCollectionView.GroupDescriptions.Remove(groupDescription);
    }

    /// <summary>
    /// Filter by category
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FilterCatgory_SelectionChanged(object sender, SelectionChangedEventArgs e) => updateProductItems();

    /// <summary>
    /// access to product item window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductItemListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.ProductItem productItem = (BO.ProductItem)ProductItemListview.SelectedItem;

        if (productItem is not null)
            new ProductItem(_cart!, productItem.ProductID, updateProductItems).Show();
    }

    /// <summary>
    /// access to cart window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ShowCart(object sender, RoutedEventArgs e)
    {
        new Cart.Cart(_cart!, updateProductItems).Show();
    }

    /// <summary>
    /// present all category
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AllCategory(object sender, RoutedEventArgs e)
    {
        Category = null;
        updateProductItems();
    }

    /// <summary>
    ///CheckBox to present by grouping
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CheckBox_Checked(object sender, RoutedEventArgs e)
    {
        ProductItemsCollectionView.GroupDescriptions.Add(groupDescription);
    }

    /// <summary>
    /// UnCheckBox to present not by category
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UnCheckBox_Checked(object sender, RoutedEventArgs e)
    {
        ProductItemsCollectionView.GroupDescriptions.Remove(groupDescription);
    }

    /// <summary>
    /// Sort the list by column
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SortByColmun(object sender, RoutedEventArgs e)
    {
        GridViewColumnHeader gridViewColumnHeader = (sender as GridViewColumnHeader)!;
        if (gridViewColumnHeader is not null)
        {
            string tag = (gridViewColumnHeader.Tag as string)!;
            ProductItemListview.Items.SortDescriptions.Clear();
            ProductItemListview.Items.SortDescriptions.Add(new SortDescription(tag, ListSortDirection.Ascending));
        }
    }

    /// <summary>
    /// Apply clicking only is the mouse on the selected item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductItemListview_MouseMove(object sender, MouseEventArgs e)
    {
        ProductItemListview.SelectedItem = null;
    }
}