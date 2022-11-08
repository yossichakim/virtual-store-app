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
        AddProduct, GetProduct, GetAllProduct, RemoveProduct, UpdateProduct
    }
    enum OrderMenu
    {
        AddOrder, GetOrder, GetAllorders, RemoveOrder, UpdateOrder
    }
    enum OrderItemMenu
    {
        AddOrderItem, GetOrderItem, GetAllOrdersItems, RemoveOrderItem, UpdateOrderItem, FindOrderItem, GetByOrderID
    }

    private static DalProduct _dalProduct = new DalProduct();
    private static DalOrder _dalOrder = new DalOrder();
    private static DalOrderItem _dalOrderItem = new DalOrderItem();

    static void Main(string[] args)
    {
        while (true)
        {
            printMainMenu();
            int choice = tryParseInt();
            MainMenu mainMenu = (MainMenu)choice;

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
                        break;
                    case MainMenu.OrderItem:
                        break;
                    default:
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
    private static void productActions()
    {
        printSubMenu("product");
        ProductMenu productMenu = (ProductMenu)tryParseInt();

        switch (productMenu)
        {
            case ProductMenu.AddProduct:
                Product product = new Product();
                addProduct(ref product);
                _dalProduct.AddProduct(product);
                break;
            case ProductMenu.GetProduct:
                Console.WriteLine("enter the product id to get:");
                _dalProduct.GetProduct(tryParseInt());
                break;
            case ProductMenu.GetAllProduct:
                _dalProduct.GetAllProduct();
                break;
            case ProductMenu.RemoveProduct:
                Console.WriteLine("enter the product id to remove:");
                _dalProduct.RemoveProduct(tryParseInt());
                break;
            case ProductMenu.UpdateProduct:
                //Product product1 = new Product();
                //updateProduct(ref product1);
                //_dalProduct.UpdateProduct(product1);
                //updateProduct();
                _dalProduct.UpdateProduct(updateProduct());
                break;
            default:
                Console.WriteLine("error - enter a number between 0 - 4");
                break;
        }
    }

    private static void printMainMenu()
    {
        Console.WriteLine(@"enter your choice:
                            1 - Product Menu
                            2 - Order Menu
                            3 - Order Item Menu
                            0 - Finish the program");
    }

    #region product actions

    private static int entityID(string entityName)
    {
        Console.WriteLine($"enter {entityName} id:");
        return tryParseInt();
    }

    private static string productName(string entityName)
    {
        Console.WriteLine($"enter {entityName} name:");
        return Console.ReadLine();
    }

    private static double entityPrice(string entityName)
    {
        Console.WriteLine($"enter {entityName} price:");
        return tryParseDouble();
    }

    private static int entityStock(string entityName)
    {
        Console.WriteLine($"enter {entityName} stock:");
        return tryParseInt();
    }

    private static void addProduct(ref Product product)
    {
        product.ProductID = entityID("product");

        product.Name = productName("product");

        product.Category = (Category)tryParseCategoty();

        product.Price = entityPrice("product");

        product.InStock = entityStock("product");
    }

    private static Product updateProduct()
    {
        Product product = _dalProduct.GetProduct(entityID("product"));
        int choice = 1;
        while (choice != 0)
        {
            Console.WriteLine(@"what do you want to update?
                            1 - product name
                            2 - product category
                            3 - product price
                            4 - product stock
                            0 - finish update");

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
                    product.InStock = entityStock("product");
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


    Order SetOrder(Order order)
    {
        Console.WriteLine("enter order id: ");
        order.OrderID = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter customer name:");
        order.CustomerName = Console.ReadLine();
        Console.WriteLine("enter customer email:");
        order.CustomerEmail = Console.ReadLine();
        Console.WriteLine("enter customer address:");
        order.CustomerAddress = Console.ReadLine();
        Console.WriteLine("enter order date in format dd/mm/yy hh/mm/ss:");
        order.OrderDate = Convert.ToDateTime(Console.ReadLine());
        Console.WriteLine("enter ship date in format dd/mm/yy hh/mm/ss:");
        order.ShipDate = Convert.ToDateTime(Console.ReadLine());
        Console.WriteLine("enter delivery date in format dd/mm/yy hh/mm/ss:");
        order.DeliveryDate = Convert.ToDateTime(Console.ReadLine());

        return order;
    }

    OrderItem SetOrderItem(OrderItem orderItem)
    {
        Console.WriteLine("enter order item id:");
        orderItem.OrderID = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter product id:");
        orderItem.OrderItemID = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter order id:");
        orderItem.OrderID = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter order item price:");
        orderItem.Price = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("enter order item amount:");
        orderItem.Price = Convert.ToInt32(Console.ReadLine());

        return orderItem;
    }

    #endregion


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

    private static void enterChoice()
    {
        Console.WriteLine("enter your choice:");
    }

    private static void printSubMenu(string entityName)
    {
        //להמשיך ....
        enterChoice();
        Console.WriteLine(@$"enter 1 for add {entityName}
                            enter 2 for ....


                                        ");
    }
}