using System.Windows;

namespace PL.Product;

/// <summary>
/// Interaction logic for Product.xaml
/// </summary>
public partial class ProductView : Window
{
    /// <summary>
    /// Access for the logical layer
    /// </summary>
    private static BLApi.IBl? s_bl = BLApi.Factory.Get();

    /// <summary>
    ///  Dependency Property for Product
    /// </summary>
    public static readonly DependencyProperty ProductDep = DependencyProperty.Register(nameof(Product),
                                                                                       typeof(BO.Product),
                                                                                      typeof(ProductView));

    /// <summary>
    /// A product object
    /// </summary>
    public BO.Product? Product { get => (BO.Product?)GetValue(ProductDep); set => SetValue(ProductDep, value); }

    /// <summary>
    /// If the product is updated, the list will be updated
    /// </summary>
    private event Action _productChanged;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="productChanged"></param>
    /// <param name="id"></param>
    public ProductView(Action productChanged, int id = 0)
    {
        InitializeComponent();
        _productChanged = productChanged;
        if (id == 0)
        {
            Product = new() { Category = BO.Category.Screens };
            UpdateProduct.Visibility = Visibility.Hidden;
        }
        else
        {
            Product = s_bl!.Product.GetProductManger(id);
            Id.IsEnabled = false;
            AddProduct.Visibility = Visibility.Hidden;
        }
    }

    /// <summary>
    /// Adding a product to the product list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddProductClick(object sender, RoutedEventArgs e)
    {
        bool flag = ValidProduct();

        if (flag == false)
            return;
        try
        {
            s_bl?.Product.AddProduct(Product!);
            MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
            _productChanged?.Invoke();
        }
        catch (BO.AddException ex)
        {
            MessageBox.Show(ex.Message + ex.InnerException!.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (BO.NoValidException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// Product update in the product list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateProductClick(object sender, RoutedEventArgs e)
    {
        bool flag = ValidProduct();

        if (flag == false)
            return;

        try
        {
            s_bl?.Product.UpdateProduct(Product!);
            MessageBox.Show("SUCCSES", "SUCCSES", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
            _productChanged?.Invoke();
        }
        catch (BO.NoFoundException ex)
        {
            MessageBox.Show(ex.Message + ex.InnerException!.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (BO.NoValidException ex)
        {
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    /// <summary>
    /// Auxiliary function for basic input correctness check when adding or updating a product
    /// </summary>
    /// <returns> If everything is fine you will return a product entity to add or update </returns>
    private bool ValidProduct()
    {
        if (!int.TryParse(Instock.Text, out int n1) ||
            !int.TryParse(Id.Text, out int n2) ||
            !double.TryParse(Price.Text, out double n3) ||
            string.IsNullOrWhiteSpace(Product!.ProductName))
        {
            MessageBox.Show("ERROR - ONE FIELD IN INCORECT INPUT", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }

        return true;
    }

    /// <summary>
    /// for input only numbers
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private new void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        e.Handled = ValidInput.ValidInputs.isValidNumber(e.Text);
    }

    private void Price_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        if (e.Text == "." && !Price.Text.Contains('.'))
        {
            e.Handled = false;
            return;
        }
        e.Handled = ValidInput.ValidInputs.isValidPrice(e.Text);
    }
}