using Dal;
using DO;

class Program
{
    enum MainMenu
    {
        Exit, Product, Order, OrderItem
    }
    enum ProductMenu
    {
        AddProduct = 1, GetProduct, GetAllProduct, RemoveProduct, UpdateProduct
    }
    enum OrderMenu
    {
        AddOrder = 1, GetOrder, GetAllorders, RemoveOrder, UpdateOrder
    }
    enum OrderItemMenu
    {
        AddOrderItem = 1, GetOrderItem, GetAllOrdersItems, RemoveOrderItem, UpdateOrderItem, FindOrderItem, GetByOrderID
    }

    private static DalProduct _dalProduct = new DalProduct();
    private static DalOrder _dalOrder = new DalOrder();
    private static DalOrderItem _dalOrderItem = new DalOrderItem();

    static void Main(string[] args)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }

    #region print function
    private static void printMainMenu()
    {
        Console.WriteLine("enter your choice:\n" +
                          "1 - Product Menu\n" +
                          "2 - Order Menu\n" +
                          "3 - Order Item Menu\n" +
                          "0 - Finish the program\n");
    }

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
    private static void enterChoice()
    {
        Console.WriteLine("enter your choice:");
    }
    #endregion

    #region product actions
    private static void productActions()
    {
        printSubMenu("product");
        ProductMenu productMenu = (ProductMenu)tryParseInt();

        switch (productMenu)
        {
            case ProductMenu.AddProduct:
                Product product = new Product();
                addProduct(ref product);
                Console.WriteLine(_dalProduct.AddProduct(product));
                break;

            case ProductMenu.GetProduct:
                Console.WriteLine("enter the product id to get:");
                Console.WriteLine(_dalProduct.GetProduct(tryParseInt()));
                break;

            case ProductMenu.GetAllProduct:
                Product[] printProducts = _dalProduct.GetAllProduct();
                foreach (var item in printProducts) Console.WriteLine(item);
                break;

            case ProductMenu.RemoveProduct:
                Console.WriteLine("enter the product id to remove:");
                _dalProduct.RemoveProduct(tryParseInt());
                break;

            case ProductMenu.UpdateProduct:
                _dalProduct.UpdateProduct(updateProduct());
                break;

            default:
                Console.WriteLine("error - enter a number between 0 - 4");
                break;
        }
    }

    #endregion

    #region product actions

    private static int entityID(string entityName)
    {
        Console.WriteLine($"enter {entityName} id:");
        return tryParseInt();
    }

    private static string productName(string entityName)
    {
        Console.WriteLine($"enter {entityName}:");
        return Console.ReadLine();
    }

    private static double entityPrice(string entityName)
    {
        Console.WriteLine($"enter {entityName} price:");
        return tryParseDouble();
    }

    private static int entityUnit(string entityName)
    {
        Console.WriteLine($"enter {entityName} units:");
        return tryParseInt();
    }

    private static void addProduct(ref Product product)
    {
        product.ProductID = entityID("product");

        product.Name = productName("product name");

        product.Category = (Category)tryParseCategoty();

        product.Price = entityPrice("product");

        product.InStock = entityUnit("product");
    }

    private static Product updateProduct()
    {
        Product product = _dalProduct.GetProduct(entityID("product"));
        int choice = 1;
        while (choice != 0)
        {
            Console.WriteLine("what do you want to update?" +
                              "1 - product name\n" +
                              "2 - product category\n" +
                              "3 - product price\n" +
                              "4 - product stock\n" +
                              "0 - finish update");

            enterChoice();
            choice = tryParseInt();
            switch (choice)
            {
                case 1:
                    product.Name = productName("product");
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

    #region order item actions
    private static void orderItemtActions()
    {
        printSubMenu("order item");
        OrderItemMenu orderItemMenu = (OrderItemMenu)tryParseInt();

        switch (orderItemMenu)
        {
            case OrderItemMenu.AddOrderItem:
                OrderItem orderItem = new OrderItem();
                addOrderItem(ref orderItem);
                Console.WriteLine(_dalOrderItem.AddOrderItem(orderItem));
                break;

            case OrderItemMenu.GetOrderItem:
                Console.WriteLine("enter the order item id to get:");
                Console.WriteLine(_dalOrderItem.GetOrderItem(tryParseInt()));
                break;

            case OrderItemMenu.GetAllOrdersItems:
                OrderItem[] printOrdersItems = _dalOrderItem.GetAllOrdersItems();
                foreach (var item in printOrdersItems) Console.WriteLine(item);
                break;

            case OrderItemMenu.RemoveOrderItem:
                Console.WriteLine("enter the order item id to remove:");
                _dalOrderItem.RemoveOrderItem(tryParseInt());
                break;

            case OrderItemMenu.UpdateOrderItem:
                _dalOrderItem.UpdateOrderItem(updateOrderItem());
                break;

            case OrderItemMenu.FindOrderItem:
                Console.WriteLine("enter the product id to find:");
                int productID = tryParseInt();
                Console.WriteLine("enter the order id to find:");
                int orderID = tryParseInt();
                OrderItem findOrderItem = _dalOrderItem.FindOrderItem(productID, orderID);
                Console.WriteLine(findOrderItem);
                break;

            case OrderItemMenu.GetByOrderID:
                Console.WriteLine("enter the order ids to find:");
                int orderIDs = tryParseInt();
                OrderItem[] printOrderIDs = _dalOrderItem.GetByOrderID(orderIDs);
                foreach (var item in printOrderIDs) Console.WriteLine(item);
                break;

            default:
                break;
        }
    }
    private static void addOrderItem(ref OrderItem orderItem)
    {
        orderItem.OrderItemID = entityID("order item");

        orderItem.OrderID = entityID("order");

        orderItem.ProductID = entityID("product");

        orderItem.Price = entityPrice("order item");

        orderItem.Amount = entityUnit("product");
    }

    private static OrderItem updateOrderItem()
    {
        OrderItem orderItem = _dalOrderItem.GetOrderItem(tryParseInt());
        int choice = 1;
        while (choice != 0)
        {
            Console.WriteLine("what do you want to update?" +
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

    #endregion item actions

    private static int tryParseInt()
    {
        int number;
        while (!int.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("error - enter a number!");
        }

        return number;
    }
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
    private static double tryParseDouble()
    {
        double number;
        while (!double.TryParse(Console.ReadLine(), out number))
        {
            Console.WriteLine("error - enter a number!");
        }

        return number;
    }

    #region order actions

    private static void orderActions()
    {
        printSubMenu("order");
        OrderMenu orderMenu = (OrderMenu)tryParseInt();

        switch (orderMenu)
        {
            case OrderMenu.AddOrder:
                Order order = new Order();
                addOrder(ref order);
                Console.WriteLine(_dalOrder.AddOrder(order));

                break;

            case OrderMenu.GetOrder:
                Console.WriteLine("enter the Order id to get:");
                Console.WriteLine(_dalOrder.GetOrder(tryParseInt()));

                break;

            case OrderMenu.GetAllorders:
                Order[] printOrder = _dalOrder.GetAllorders();
                foreach (var item in printOrder)
                {
                    Console.WriteLine(item);
                }
                break;

            case OrderMenu.RemoveOrder:
                Console.WriteLine("enter the Order id to remove:");
                _dalOrder.RemoveOrder(tryParseInt());

                break;

            case OrderMenu.UpdateOrder:

                _dalOrder.UpdateOrder(updateOrder());

                break;

            default:
                Console.WriteLine("error - enter a number between 0 - 4");

                break;

        }

    }
    private static void addOrder(ref Order order)
    {
        order.OrderID = entityID("order");

        order.CustomerName = productName("customer name");

        order.CustomerAddress = productName("customer address");

        order.CustomerEmail = productName("customer email");

        order.OrderDate = tryParseDate("order");

        order.ShipDate = tryParseDate("ship");

        order.DeliveryDate = tryParseDate("delivery");
    }
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
    private static Order updateOrder()
    {
        Order order = _dalOrder.GetOrder(entityID("order"));
        int choice = 1;
        while (choice != 0)
        {
            Console.WriteLine(@"what do you want to update?
                            1 - customer name 
                            2 - customer address
                            3 - costumer email
                            4 - order date
                            5 - ship date
                            6 - delivery date
                            0 - finish update");

            enterChoice();
            choice = tryParseInt();
            switch (choice)
            {
                case 1:
                    order.CustomerName = productName("customer name");
                    break;
                case 2:
                    order.CustomerAddress = productName("customer address");
                    break;
                case 3:
                    order.CustomerEmail = productName("customer email");
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
    #endregion  order actions
}