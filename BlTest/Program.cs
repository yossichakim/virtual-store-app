using BLApi;
using BlImplementation;
using BO;

namespace BlTest;

internal class Program
{
    /// <summary>
    /// main menu
    /// </summary>
    private enum MainMenu
    {
        Exit, Product, Order, Cart
    }

    /// <summary>
    /// Sub menu for products
    /// </summary>
    private enum ProductMenu
    {
        GetProductList = 1, GetProductManger, GetProductCostumer, AddProduct, RemoveProduct, UpdateProduct
    }

    /// <summary>
    /// Sub menu for orders
    /// </summary>
    private enum OrderMenu
    {
        GetOrderList = 1, GetOrderDetails, ShippingUpdate, DeliveryUpdate, OrderTrackingManger
    }

    /// <summary>
    /// Shopping cart submenu
    /// </summary>
    private enum CartMenu
    {
        AddProductToCart = 1, UpdateAmount, ConfirmedOrder
    }

    /// <summary>
    /// A variable to implement the logical functions
    /// </summary>
    private static IBl _bl = new Bl();

    /// <summary>
    /// Shopping basket show
    /// </summary>
    private static Cart _cart = new();

    /// <summary>
    /// main plan
    /// </summary>
    /// <param name="args"></param>
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
            catch (AddException ex) when (ex.InnerException is not null)
            {
                Console.WriteLine(ex.Message + ex.InnerException.Message);
            }
            catch (NoFoundException ex) when (ex.InnerException is not null)
            {
                Console.WriteLine(ex.Message + ex.InnerException.Message);
            }
            catch (NoValidException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ErrorDeleteException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ErrorUpdateException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ErrorUpdateCartException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    #region print functions

    /// <summary>
    /// Print main menu
    /// </summary>
    private static void printMainMenu()
    {
        Console.WriteLine("enter your choice:\n" +
                          "1 - Product Menu\n" +
                          "2 - Order Menu\n" +
                          "3 - Cart Menu\n" +
                          "0 - Finish the program\n");
    }

    /// <summary>
    /// Print submenu
    /// </summary>
    /// <param name="entityName"></param>
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
                      $"enter 5 to remove {entityName}\n" +
                      $"enter 6 to update {entityName} \n");
                break;

            case "order":

                Console.WriteLine($"enter 1 to get all {entityName}s\n" +
                      $"enter 2 to get {entityName} details\n" +
                      $"enter 3 to shipping update\n" +
                      $"enter 4 to delivery update\n" +
                      $"enter 5 to {entityName} tracking for manger\n");
                break;

            case "cart":

                Console.WriteLine($"enter 1 to add product to {entityName}\n" +
                      $"enter 2 to update amount\n" +
                      $"enter 3 to confirmed order\n");
                break;
        }
    }

    /// <summary>
    /// A request from the user to choose
    /// </summary>
    private static void enterChoice()
    {
        Console.WriteLine("enter your choice:");
    }

    #endregion print functions

    #region product actions

    /// <summary>
    /// Performing the operations for a product for the buyer and the manager
    /// </summary>
    private static void productActions()
    {
        printSubMenu("product");
        ProductMenu productMenu = (ProductMenu)tryParseInt();

        switch (productMenu)
        {
            case ProductMenu.GetProductList:
                IEnumerable<ProductForList> printProducts = _bl.Product.GetProductList()!;
                foreach (var item in printProducts) Console.WriteLine(item);
                break;

            case ProductMenu.GetProductManger:
                Console.WriteLine("enter the product id to get:");
                Console.WriteLine(_bl.Product.GetProductManger(tryParseInt()));
                break;

            case ProductMenu.GetProductCostumer:
                Console.WriteLine("enter the product id to get:");
                Console.WriteLine(_bl.Product.GetProductCostumer(tryParseInt(), _cart));
                break;

            case ProductMenu.AddProduct:
                {
                    Product product = new();
                    addProduct(product);
                    _bl.Product.AddProduct(product);
                    break;
                }

            case ProductMenu.RemoveProduct:
                Console.WriteLine("enter the product id to remove product:");
                _bl.Product.RemoveProduct(tryParseInt());
                break;

            case ProductMenu.UpdateProduct:
                {
                    _bl.Product.UpdateProduct(updateProduct());
                    break;
                }

            default:
                Console.WriteLine("Error - not in options");
                break;
        }
    }

    /// <summary>
    /// Product input
    /// </summary>
    /// <param name="product"></param>
    private static void addProduct(Product product)
    {
        product.ProductID = entityID("product");

        product.ProductName = entityName("product name");

        product.Category = (Category)tryParseCategoty();

        product.ProductPrice = entityPrice("product");

        product.InStock = entityUnit("product");
    }

    /// <summary>
    /// update product according to the user's request
    /// </summary>
    /// <returns>Product</returns>
    private static Product updateProduct()
    {
        Product product = _bl.Product.GetProductManger(entityID("product"));
        int choice = 1;
        while (choice != 0)
        {
            Console.WriteLine("what do you want to update?\n" +
                              "1 - product name\n" +
                              "2 - product price\n" +
                              "3 - product stock\n" +
                              "4 - product category\n" +
                              "0 - finish update");

            enterChoice();
            choice = tryParseInt();
            switch (choice)
            {
                case 1:
                    product.ProductName = entityName("product name");
                    break;

                case 2:
                    product.ProductPrice = entityPrice("product");
                    break;

                case 3:
                    product.InStock = entityUnit("product");
                    break;

                case 4:
                    product.Category = (Category)tryParseCategoty();
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("enter a number between 0 - 4");
                    break;
            }
        }
        return product;
    }

    #endregion product actions

    #region order actions

    /// <summary>
    /// Performing actions for an order
    /// </summary>
    private static void orderActions()
    {
        printSubMenu("order");
        OrderMenu orderMenu = (OrderMenu)tryParseInt();

        switch (orderMenu)
        {
            case OrderMenu.GetOrderList:
                IEnumerable<OrderForList> printOrders = _bl.Order.GetOrderList()!;
                foreach (var item in printOrders) Console.WriteLine(item);
                break;

            case OrderMenu.GetOrderDetails:
                Console.WriteLine("enter the order id to get:");
                Console.WriteLine(_bl.Order.GetOrderDetails(tryParseInt()));
                break;

            case OrderMenu.ShippingUpdate:
                Console.WriteLine("enter the order id:");
                Console.WriteLine(_bl.Order.ShippingUpdate(tryParseInt()));
                break;

            case OrderMenu.DeliveryUpdate:
                Console.WriteLine("enter the order id:");
                Console.WriteLine(_bl.Order.DeliveryUpdate(tryParseInt()));
                break;

            case OrderMenu.OrderTrackingManger:
                Console.WriteLine("enter the order id:");
                Console.WriteLine(_bl.Order.OrderTrackingManger(tryParseInt()));
                break;

            default:
                Console.WriteLine("Error - not in options");
                break;
        }
    }

    #endregion order actions

    #region cart action

    /// <summary>
    /// Performing actions for shopping basket
    /// </summary>
    private static void cartActions()
    {
        printSubMenu("cart");
        CartMenu cartMenu = (CartMenu)tryParseInt();

        switch (cartMenu)
        {
            case CartMenu.AddProductToCart:
                Console.WriteLine("enter the product id:");
                Console.WriteLine(_bl.Cart.AddProductToCart(_cart, tryParseInt()));
                break;

            case CartMenu.UpdateAmount:
                Console.WriteLine("enter the product id:");
                int productId = tryParseInt();
                int newAmount = entityUnit("amount");
                Console.WriteLine(_bl.Cart.UpdateAmount(_cart, productId, newAmount));
                break;

            case CartMenu.ConfirmedOrder:
                Console.WriteLine("enter the name:");
                _cart.CustomerName = Console.ReadLine();
                Console.WriteLine("enter the email:");
                _cart.CustomerEmail = Console.ReadLine();
                Console.WriteLine("enter the address:");
                _cart.CustomerAddress = Console.ReadLine();
                _bl.Cart.ConfirmedOrder(_cart);
                break;

            default:
                Console.WriteLine("Error - not in options");
                break;
        }
    }

    #endregion cart action

    #region general actions

    /// <summary>
    /// Input a valid int number
    /// </summary>
    /// <returns>int</returns>
    private static int tryParseInt()
    {
        int number;
        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("error - enter a number!");
        }

        return number;
    }

    /// <summary>
    /// Input a valid Category
    /// </summary>
    /// <returns>int</returns>
    private static int tryParseCategoty()
    {
        int number;
        do
        {
            Console.WriteLine("enter product category: a number between 0 - 4");
            number = tryParseInt();
        } while (number < 0 || number > 4);

        return number;
    }

    /// <summary>
    /// Input a valid double number
    /// </summary>
    /// <returns>double</returns>
    private static double tryParseDouble()
    {
        double number;
        while (!double.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("error - enter a number!");
        }

        return number;
    }

    /// <summary>
    /// Input a valid ID
    /// </summary>
    /// <param name="entityName"></param>
    /// <returns>int</returns>
    private static int entityID(string entityName)
    {
        Console.WriteLine($"enter {entityName} id:");
        return tryParseInt();
    }

    /// <summary>
    /// Input a valid name
    /// </summary>
    /// <param name="entityName"></param>
    /// <returns>string</returns>
    private static string entityName(string entityName)
    {
        string? str;
        Console.WriteLine($"enter {entityName}:");
        str = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(str))
        {
            Console.WriteLine("enter a not empty string");
            str = Console.ReadLine();
        }

        return str;
    }

    /// <summary>
    /// Input a valid price
    /// </summary>
    /// <param name="entityName"></param>
    /// <returns>double</returns>
    private static double entityPrice(string entityName)
    {
        Console.WriteLine($"enter {entityName} price:");
        return tryParseDouble();
    }

    /// <summary>
    /// Input a valid unit
    /// </summary>
    /// <param name="entityName"></param>
    /// <returns>int</returns>
    private static int entityUnit(string entityName)
    {
        Console.WriteLine($"enter {entityName} units:");
        return tryParseInt();
    }

    #endregion general actions
}