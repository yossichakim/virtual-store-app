using DO;

namespace Dal;
static class DataSource
{
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

    }
    private static void initOrderItems()
    {

    }

}
