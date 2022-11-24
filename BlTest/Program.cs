using BLApi;
using BlImplementation;
using BO;
using DalApi;

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
        while (true)
        {
            printMainMenu();
            MainMenu mainMenu = (MainMenu)tryParseInt();

            try
            {
                switch (mainMenu)
                {
                    case MainMenu.Exit:
                        Console.WriteLine("You choose to finish bye-bye \n");
                        return;

                    case MainMenu.Product:
                        productActions();
                        break;

                    case MainMenu.Order:
                        orderActions();
                        break;

                    case MainMenu.Cart:
                        cartActions();

                        break;
                    default:
                        Console.WriteLine("\nenter a number between 0 - 3\n");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
    #region general Actions
    private static int tryParseInt()
    {
        int number;
        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("error - enter a number!");
        }

        return number;
    }


    #endregion general Actions

    #region print functions
    private static void printMainMenu()
    {
        Console.WriteLine("enter your choice:\n" +
                          "1 - Product Menu\n" +
                          "2 - Order Menu\n" +
                          "3 - Cart Menu\n" +
                          "0 - Finish the program\n");
    }

    private static void printSubMenu(string entityName)
    {
        enterChoice();
        switch (entityName)
        {

            case "product":

                Console.WriteLine($"enter 1 to get all {entityName}s\n" +
                      $"enter 2 to get {entityName} manger\n" +
                      $"enter 3 to get {entityName} costumer\n" +
                      $"enter 4 to add {entityName}\n" +
                      $"enter 5 to remove {entityName}" +
                      $"enter 6 to update {entityName}");
                break;

            case "order":

                Console.WriteLine($"enter 1 to get all {entityName}s\n" +
                      $"enter 2 to get {entityName} details\n" +
                      $"enter 3 to shipping update\n" +
                      $"enter 4 to delivery update\n" +
                      $"enter 5 to {entityName} tracking for manger");
                //$"enter 6 to update {entityName}");
                break;

            case "cart":

                Console.WriteLine($"enter 1 to add product to {entityName}\n" +
                      $"enter 2 to update amount\n" +
                      $"enter 3 to confirmed order\n" +
                      $"enter 4 to delivery update");
                break;

        }

    }

    private static void enterChoice()
    {
        Console.WriteLine("enter your choice:");
    }


    #endregion print functions

    #region product actions

    private static void productActions()
    {
        printSubMenu("product");
        ProductMenu productMenu = (ProductMenu)tryParseInt();

        switch (productMenu)
        {
            case ProductMenu.GetProductList:
                IEnumerable<ProductForList> printProducts = bl.Product.GetProductList();
                foreach (var item in printProducts) Console.WriteLine(item);
                break;

            case ProductMenu.GetProductManger:
                Console.WriteLine("enter the product id to get:");
                Console.WriteLine(bl.Product.GetProductManger(tryParseInt()));
                break;
            case ProductMenu.GetProductCostumer:
                Console.WriteLine("enter the product id to get:");
                Console.WriteLine(bl.Product.GetProductCostumer(tryParseInt(), bl.Cart cart));
                break;
            case ProductMenu.AddProduct:
                break;
            case ProductMenu.RemoveProduct:
                break;
            case ProductMenu.UpdateProduct:
                break;
            default:
                break;
        }

    }
    #endregion

    #region order actions
    #endregion order actions

    #region cart actions
    #endregion cart actions
}