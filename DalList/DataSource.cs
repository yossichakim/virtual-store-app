using DO;

namespace Dal;
static class DataSource
{

    internal static class Config
    {

        //internal static int _indexProducts { set; get; } = 0;
        //internal static int _indexOrders { set; get; } = 0;
        //internal static int _indexOrdersItems { set; get; } = 0;

        private static int _orderItemID = 0;

        private static int _orderID = 0;

        internal static int _orderItemIDGet => _orderItemID++;

        internal static int _orderIDGet => _orderID++;
    }


    static DataSource() { s_Initialize(); } //dtor

    private static readonly Random _random = new Random();
    internal static List<Product> Products { get; set; } = new List<Product>(50);
    internal static List<Order> Orders { get; set; } = new List<Order>(100);
    internal static List<OrderItem> OrderItems { get; set; } = new List<OrderItem>(200);

    private static void s_Initialize()
    {
        initProducts();
        initOrders();
        initOrderItems();
    }

    private static void initProducts()
    {
        int productID = 111111;
        for (int i = 0; i < 25; i++)
        {
            Product product = new Product { ProductID = productID++, };


        }
    }
    private static void initOrders()
    {

        for (int i = 0; i < 40; i++)
        {
            Order order = new Order { OrderID = Config._orderIDGet, };
        }

    }

    private List<string> getFirstName()
    {
        return new List<string> { };
    }

    private static void initOrderItems()
    {

    }

    

    
}
