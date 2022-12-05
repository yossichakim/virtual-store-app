using Dal;
using DalApi;
using DO;

/// <summary>
/// Prints and updates in arrays according to the user's request
/// </summary>
internal class Program
{
    /// <summary>
    /// The main menu of user's request
    /// </summary>
    private enum MainMenu
    {
        Exit, Product, Order, OrderItem
    }

    /// <summary>
    /// The product menu of user's request
    /// </summary>
    private enum ProductMenu
    {
        AddProduct = 1, GetProduct, GetAllProduct, RemoveProduct, UpdateProduct
    }

    /// <summary>
    /// The order menu of user's request
    /// </summary>
    private enum OrderMenu
    {
        AddOrder = 1, GetOrder, GetAllorders, RemoveOrder, UpdateOrder
    }

    /// <summary>
    /// The order item menu of user's request
    /// </summary>
    private enum OrderItemMenu
    {
        AddOrderItem = 1, GetOrderItem, GetAllOrdersItems, RemoveOrderItem, UpdateOrderItem, FindOrderItem, GetByOrderID
    }

    private static IDal dal = new DalList();

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

                    case MainMenu.OrderItem:
                        orderItemtActions();
                        break;

                    default:
                        Console.WriteLine("\nenter a number between 0 - 3\n");
                        break;
                }
            }
            catch (AddException ex)
            {
                Console.WriteLine(ex.Message);
            }

            catch (NoFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    #region print function

    /// <summary>
    /// Printing the main menu to the user
    /// </summary>
    private static void printMainMenu()
    {
        Console.WriteLine("enter your choice:\n" +
                          "1 - Product Menu\n" +
                          "2 - Order Menu\n" +
                          "3 - Order Item Menu\n" +
                          "0 - Finish the program\n");
    }

    /// <summary>
    /// Printing the sub menu to the user
    /// </summary>
    /// <param name="entityName"></param>
    private static void printSubMenu(string entityName)
    {
        enterChoice();
        Console.WriteLine($"enter 1 to add {entityName}\n" +
                          $"enter 2 to get {entityName}\n" +
                          $"enter 3 to get all {entityName}s\n" +
                          $"enter 4 to remove {entityName}\n" +
                          $"enter 5 to update {entityName}");
        if (entityName == "order item")
        {
            Console.WriteLine($"enter 6 to find {entityName}\n" +
                              $"enter 7 to get all {entityName}s by order ID");
        }
    }

    /// <summary>
    /// Request a choice from the user
    /// </summary>
    private static void enterChoice()
    {
        Console.WriteLine("enter your choice:");
    }

    #endregion print function

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
            Console.WriteLine("ERROR - ENTER A NUMBER!");
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

    #region product actions

    /// <summary>
    /// Printing a menu of product and carrying out the user's request
    /// </summary>
    private static void productActions()
    {
        printSubMenu("product");
        ProductMenu productMenu = (ProductMenu)tryParseInt();

        switch (productMenu)
        {
            case ProductMenu.AddProduct:
                Product product = new Product();
                addProduct(ref product);
                Console.WriteLine(dal.Product.Add(product));
                break;

            case ProductMenu.GetProduct:
                Console.WriteLine("enter the product id to get:");
                int id = tryParseInt();
                Console.WriteLine(dal.Product.Get(element => id == element?.ProductID));
                break;

            case ProductMenu.GetAllProduct:
                IEnumerable<Product?> printProducts = dal.Product.GetAll();
                foreach (var item in printProducts) Console.WriteLine(item);
                break;

            case ProductMenu.RemoveProduct:
                Console.WriteLine("enter the product id to remove:");
                dal.Product.Delete(tryParseInt());
                break;

            case ProductMenu.UpdateProduct:
                dal.Product.Update(updateProduct());
                break;

            default:
                Console.WriteLine("ERROR - NOT IN OPTION");
                break;
        }
    }

    /// <summary>
    /// Creates an product by receiving from the user
    /// </summary>
    /// <param name="product"></param>
    private static void addProduct(ref Product product)
    {
        product.ProductID = entityID("product");

        product.Name = entityName("product name");

        product.Category = (Category)tryParseCategoty();

        product.Price = entityPrice("product");

        product.InStock = entityUnit("product");
    }

    /// <summary>
    /// update product according to the user's request
    /// </summary>
    /// <returns>Product</returns>
    private static Product updateProduct()
    {
        Product product = dal.Product.Get(entityID("product"));
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
                    product.Name = entityName("product name");
                    break;

                case 2:
                    product.Price = entityPrice("product");
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
    /// Printing a menu of order and carrying out the user's request
    /// </summary>
    private static void orderActions()
    {
        printSubMenu("order");
        OrderMenu orderMenu = (OrderMenu)tryParseInt();

        switch (orderMenu)
        {
            case OrderMenu.AddOrder:
                Order order = new Order();
                addOrder(ref order);
                Console.WriteLine(dal.Order.Add(order));
                break;

            case OrderMenu.GetOrder:
                Console.WriteLine("enter the Order id to get:");
                int id = tryParseInt();
                Console.WriteLine(dal.Order.Get(element => id == element?.OrderID));
                break;

            case OrderMenu.GetAllorders:
                IEnumerable<Order?> printOrder = dal.Order.GetAll();
                foreach (var item in printOrder) Console.WriteLine(item);
                break;

            case OrderMenu.RemoveOrder:
                Console.WriteLine("enter the Order id to remove:");
                dal.Order.Delete(tryParseInt());
                break;

            case OrderMenu.UpdateOrder:

                dal.Order.Update(updateOrder());
                break;

            default:
                Console.WriteLine("ERROR - NOT IN OPTION");
                break;
        }
    }

    /// <summary>
    /// Creates an order by receiving from the user
    /// </summary>
    /// <param name="order"></param>
    private static void addOrder(ref Order order)
    {
        order.OrderID = entityID("order");

        order.CustomerName = entityName("customer name");

        order.CustomerAddress = entityName("customer address");

        order.CustomerEmail = entityName("customer email");

        order.OrderDate = tryParseDate("order");

        order.ShipDate = tryParseDate("ship");

        order.DeliveryDate = tryParseDate("delivery");
    }

    /// <summary>
    /// Input of a valid date
    /// </summary>
    /// <param name="entityName"></param>
    /// <returns> DateTime </returns>
    private static DateTime tryParseDate(string entityName)
    {
        Console.WriteLine($"enter {entityName} date:");
        DateTime date;
        while (!DateTime.TryParse(Console.ReadLine(), out date))
        {
            Console.WriteLine("error - enter a valid date!");
        }

        return date;
    }

    /// <summary>
    /// Order update according to the user's request
    /// </summary>
    /// <returns> Order </returns>
    private static Order updateOrder()
    {
        Order order = dal.Order.Get(entityID("order"));
        int choice = 1;
        while (choice != 0)
        {
            Console.WriteLine($"what do you want to update?\n" +
                                $"1 - customer name \n" +
                                $"2 - customer address\n" +
                                $"3 - costumer email\n" +
                                $"4 - order date\n" +
                                $"5 - ship date\n" +
                                $"6 - delivery date\n" +
                                $"0 - finish update\n");

            enterChoice();
            choice = tryParseInt();
            switch (choice)
            {
                case 1:
                    order.CustomerName = entityName("customer name");
                    break;

                case 2:
                    order.CustomerAddress = entityName("customer address");
                    break;

                case 3:
                    order.CustomerEmail = entityName("customer email");
                    break;

                case 4:
                    order.OrderDate = tryParseDate("order");
                    break;

                case 5:
                    order.ShipDate = tryParseDate("ship");
                    break;

                case 6:
                    order.DeliveryDate = tryParseDate("delivery");
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("enter a number between 0 - 6");
                    break;
            }
        }
        return order;
    }

    #endregion order actions

    #region order item actions

    /// <summary>
    /// Printing a menu of order item and carrying out the user's request
    /// </summary>
    private static void orderItemtActions()
    {
        printSubMenu("order item");
        OrderItemMenu orderItemMenu = (OrderItemMenu)tryParseInt();

        switch (orderItemMenu)
        {
            case OrderItemMenu.AddOrderItem:
                OrderItem orderItem = new OrderItem();
                addOrderItem(ref orderItem);
                Console.WriteLine(dal.OrderItem.Add(orderItem));
                break;

            case OrderItemMenu.GetOrderItem:
                Console.WriteLine("enter the order item id to get:");
                int id = tryParseInt();
                Console.WriteLine(dal.OrderItem.Get(element => element?.OrderItemID == id));
                break;

            case OrderItemMenu.GetAllOrdersItems:
                IEnumerable<OrderItem?> printOrdersItems = dal.OrderItem.GetAll();
                foreach (var item in printOrdersItems) Console.WriteLine(item);
                break;

            case OrderItemMenu.RemoveOrderItem:
                Console.WriteLine("enter the order item id to remove:");
                dal.OrderItem.Delete(tryParseInt());
                break;

            case OrderItemMenu.UpdateOrderItem:
                dal.OrderItem.Update(updateOrderItem());
                break;

            case OrderItemMenu.FindOrderItem:
                Console.WriteLine("enter the product id to find:");
                int productID = tryParseInt();
                Console.WriteLine("enter the order id to find:");
                int orderID = tryParseInt();
                OrderItem? findOrderItem = dal.OrderItem.Get(element => element?.ProductID == productID && element?.OrderID == orderID);
                Console.WriteLine(findOrderItem);
                break;

            case OrderItemMenu.GetByOrderID:
                Console.WriteLine("enter the order ids to find:");
                int orderIDs = tryParseInt();
                IEnumerable<OrderItem?> printOrderIDs = dal.OrderItem.GetAll(element => element?.OrderID == orderIDs);
                foreach (var item in printOrderIDs) Console.WriteLine(item);
                break;

            default:
                Console.WriteLine("ERROR - NOT IN OPTION");
                break;
        }
    }

    /// <summary>
    /// Creates an order item by receiving from the user
    /// </summary>
    /// <param name="orderItem"></param>
    private static void addOrderItem(ref OrderItem orderItem)
    {
        orderItem.OrderItemID = entityID("order item");

        orderItem.OrderID = entityID("order");

        orderItem.ProductID = entityID("product");

        orderItem.Price = entityPrice("order item");

        orderItem.Amount = entityUnit("product");
    }

    /// <summary>
    /// Order item update according to the user's request
    /// </summary>
    /// <returns>OrderItem</returns>
    private static OrderItem updateOrderItem()
    {
        OrderItem orderItem = dal.OrderItem.Get(tryParseInt());
        int choice = 1;
        while (choice != 0)
        {
            Console.WriteLine("what do you want to update?\n" +
                              "1 - order id\n" +
                              "2 - product id\n" +
                              "3 - product price\n" +
                              "4 - product amount\n" +
                              "0 - finish update");
            enterChoice();
            choice = tryParseInt();
            switch (choice)
            {
                case 1:
                    orderItem.OrderID = entityID("order");
                    break;

                case 2:
                    orderItem.ProductID = entityID("product");
                    break;

                case 3:
                    orderItem.Amount = entityUnit("product");
                    break;

                case 4:
                    orderItem.Price = entityPrice("product");
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("enter a number between 0 - 4");
                    break;
            }
        }
        return orderItem;
    }

    #endregion order item actions
}