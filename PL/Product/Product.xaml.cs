using BLApi;
using BO;
using System;
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
    private IBl _bl;

    /// <summary>
    /// Constructor for a window to add a product
    /// </summary>
    /// <param name="bl"></param>
    public ProductView(IBl bl)
    {
        InitializeComponent();
        _bl = bl;
        Catgory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        UpdateProduct.Visibility = Visibility.Hidden;
    }

    /// <summary>
    /// Constructor for a window to update a product
    /// </summary>
    /// <param name="bl"></param>
    public ProductView(IBl bl, int updateProductID)
    {
        InitializeComponent();
        _bl = bl;
        Catgory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        BO.Product product = _bl.Product.GetProductManger(updateProductID);
        Id.Text = updateProductID.ToString();
        Id.IsEnabled = false;
        Name.Text = product.ProductName;
        Catgory.SelectedItem = product.Category;
        Price.Text = product.ProductPrice.ToString();
        Instock.Text = product.InStock.ToString();
        AddProduct.Visibility = Visibility.Hidden;
    }

    /// <summary>
    /// Adding a product to the product list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddProductClick(object sender, RoutedEventArgs e)
    {
        BO.Product? product = ValidProduct();
        if (product == null)
            return;
        try
        {
            _bl.Product.AddProduct(product);
            MessageBox.Show("SUCCSES");
            this.Close();
        }
        catch (AddException ex)
        {
            MessageBox.Show(ex.Message + ex.InnerException!.Message);
        }
        catch (NoValidException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// Product update in the product list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateProductClick(object sender, RoutedEventArgs e)
    {
        BO.Product? product = ValidProduct();

        if (product == null)
            return;

        try
        {
            _bl.Product.UpdateProduct(product);
            MessageBox.Show("SUCCSES");
            this.Close();
        }
        catch (NoFoundException ex)
        {
            MessageBox.Show(ex.Message + ex.InnerException!.Message);
        }
        catch (NoValidException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// Auxiliary function for basic input correctness check when adding or updating a product
    /// </summary>
    /// <returns> If everything is fine you will return a product entity to add or update </returns>
    private BO.Product? ValidProduct()
    {
        BO.Product? product;

        if (!int.TryParse(Instock.Text, out int n1) ||
            !int.TryParse(Id.Text, out int n2) ||
            !double.TryParse(Price.Text, out double n3) ||
            string.IsNullOrWhiteSpace(Name.Text))
        {
            MessageBox.Show("ERROR - ONE FIELD IN INCORECT INPUT");
            return null;
        }

        product = new BO.Product()
        {
            ProductID = int.Parse(Id.Text),
            ProductName = Name.Text,
            Category = (BO.Category)Catgory.SelectedItem,
            ProductPrice = double.Parse(Price.Text),
            InStock = int.Parse(Instock.Text)
        };
        return product;
    }
}