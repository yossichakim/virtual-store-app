using DO;

namespace Dal;
static class DataSource
{
    static DataSource() { s_Initialize(); } //ctor

    private static readonly Random _random = new Random();

    internal static List<Product> Products { get; set; } = new List<Product>(50);
    internal static List<Order> Orders { get; set; } = new List<Order>(100);
    internal static List<OrderItem> OrderItems { get; set; } = new List<OrderItem>(200);

    private static void s_Initialize()
    {
        initProductsByCategory();
        initOrders();
        initOrderItems();
    }

    private static void initProductsByCategory()
    {
        Dictionary<Category, List<string>> names = new Dictionary<Category, List<string>>
        {
            { Category.Screens, new List<string> {"galksy8", "lg7"} } ,
              { Category.Printers, new List<string> {} },
               { Category.Computers, new List<string> {} },
                { Category.TV, new List<string> {} },
                 { Category.Phones, new List<string> {} },
        };

        initProducts(names);
    }

    private static void initProducts(Dictionary<Category, List<string>> names)
    {
        int _initProductID = 100000;

        foreach (var catgory in names)
        {
            int count = catgory.Value.Count();

            int pre = (int)(count * 0.05) + 1;

            foreach (var name in catgory.Value)
            {
                Product product = new Product
                {
                    ProductID = _initProductID++,
                    Category = catgory.Key,
                    Name = name,
                    InStock = pre-- > 0 ? 0 : _random.Next(1, 70000),//ask
                };

                product.Price = product.Category switch
                {
                    Category.Screens => _random.Next(699, 2999),
                    Category.Phones => _random.Next(499, 4999),
                    Category.Computers => _random.Next(999, 7999),
                    Category.Printers => _random.Next(399, 3999),
                    Category.TV => _random.Next(1999, 9999),
                    _ => throw new NotImplementedException()
                };
            }
        }

        for (int i = 0; i < 25; i++)
        {
            Product product = new Product
            {
                ProductID = _initProductID++,
                Category = (Category)_random.Next(0, 5),
                InStock = i + (i % 5),//ask
            };

            product.Price = product.Category switch
            {
                Category.Screens => _random.Next(699, 2999),
                Category.Phones => _random.Next(499, 4999),
                Category.Computers => _random.Next(999, 7999),
                Category.Printers => _random.Next(399, 3999),
                Category.TV => _random.Next(1999, 9999),
                _ => throw new NotImplementedException()
            };
        }
    }
    private static void initOrders()
    {

    }
    private static void initOrderItems()
    {

    }
}
