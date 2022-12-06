using BLApi;
using BO;
using System;
using System.Windows;
namespace PL.Product;

/// <summary>
/// Interaction logic for Product.xaml
/// </summary>
public partial class Product : Window
{
    private IBl _bl;

    public Product(IBl bl)
    {
        InitializeComponent();
        _bl = bl;
        Catgory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        UpdateProduct.Visibility = Visibility.Hidden;
    }

    public Product(IBl bl, int updateProductID )
    {
        InitializeComponent();
        _bl = bl;
        Catgory.ItemsSource = Enum.GetValues(typeof(BO.Category));
        BO.Product a = _bl.Product.GetProductManger(updateProductID);
        Id.Text = updateProductID.ToString();
        Id.IsEnabled = false;
        Name.Text = a.ProductName;
        Catgory.SelectedItem = a.Category;
        Price.Text = a.ProductPrice.ToString();
        Instock.Text = a.InStock.ToString();
        AddProduct.Visibility = Visibility.Hidden;
    }

    /// <summary>
    ///
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
            MessageBox.Show("The transaction completed successfully");
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

    private void UpdateProductClick(object sender, RoutedEventArgs e)
    {
        BO.Product? product = ValidProduct();

        if (product == null)
            return;

        try
        {
            _bl.Product.UpdateProduct(product);
            MessageBox.Show("The transaction completed successfully");
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

    private BO.Product? ValidProduct()
    {
        BO.Product? product ;

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