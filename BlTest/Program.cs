
namespace BlTest;

internal class Program
{
    private enum MainMenu
    {
        Exit, Product, Order, Cart
    }

    private enum ProductMenu
    {
        GetProductList = 1, GetProductManger, GetProductCostumer, AddProduct, RemoveProduct, UpdateProduct
    }

    private enum OrderMenu
    {
        GetOrderList = 1, GetOrderDetails, ShippingUpdate, DeliveryUpdate, OrderTrackingManger
    }

    private enum CartMenu
    {
        AddProductToCart = 1, UpdateAmount, ConfirmedOrder
    }

    private static IBl bl = new Bl();
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}