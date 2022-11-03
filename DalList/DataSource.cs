using DO;
using System.Diagnostics;

namespace Dal;
static class DataSource
{
   
    private static readonly Random _random = new Random();
    internal static Product[] products = new Product[50];
    internal static Order[] orders = new Order[100];
    internal static OrderItem[] orderItems = new OrderItem[200];

    internal static class Config
    {

        internal static int _indexProducts = 0;
        internal static int _indexOrders = 0;
        internal static int _indexOrdersItems = 0;

        private static int _orderItemID = 0;
        private static int _orderID = 0;

        internal static int _orderItemIDGet => _orderItemID++;
        internal static int _orderIDGet => _orderID++;
    }

     static DataSource() { s_Initialize(); } //ctor

    private static void s_Initialize()
    {
        initProducts();
        initOrders();
        initOrderItems();
    }

    private static void initProducts()
    {
        int _initProductID = 100000;
        string[] _prodactName = { "LG 34\"", "SAMSUNG 22\"", "LENOVO 29\"", "BENQ 24\"", "DELL 21.5\"",
                                        "OPPO", "ONE PLUS", "HONOR", "SAMSUNG", "APPLE",
                                        "APPLE", "ASUS", "LENOVO", "DELL", "MSI",
                                        "HP", "CANON", "FUJIFILM", "AIMO", "EPSON",
                                         "SONY", "XIAOMI", "TCL", "AIWA", "TOSHIBA"
                                        };
        int _indexName = 0;

        for (int i = 0; i < 25; i++)
        {
            Product product = new Product
            {
                ProductID = _initProductID++,
                //Category = (Category)_random.Next(0, 5),
                InStock = i + (i % 5),//ask
            };

            product.Category = i switch
            {
                < 5 => Category.Screens,
                < 10 => Category.Phones,
                < 15 => Category.Computers,
                < 20 => Category.Printers,
                < 25 => Category.TV,
                _ => throw new NotImplementedException()
            };

            product.Name = _prodactName[_indexName++];
            product.Price = product.Category switch
            {
                Category.Screens => _random.Next(699, 2999),
                Category.Phones => _random.Next(499, 4999),
                Category.Computers => _random.Next(999, 7999),
                Category.Printers => _random.Next(399, 3999),
                Category.TV => _random.Next(1999, 9999),
                _ => throw new NotImplementedException()
            };
            products[Config._indexProducts++] = product;
        }
    }

    private static void initOrders()
    {
        string[] _costumerName = {"Emlynn Devitt",
    "Darill Aspray",
    "Gordie Jendrusch",
    "Glenna Ruppert",
    "Aubry Garrattley",
    "Torr Renton",
    "Marie-jeanne De Beneditti",
    "Jean Bartali",
    "Amil Cockrell",
    "Cristie Tanguy",
    "Lucas Spuner",
    "Blake Russan",
    "Jesselyn Munsey",
    "Ernest Escott",
    "Nickola Caffin",
    "Elka Keyte",
    "Silvan Casassa",
    "Sigismondo Ubsdale",
    "Violetta Gyde",
    "Phillipe Carbry",
    "Megan Vasyunin",
    "Betta Von Helmholtz",
    "Marlow Braden",
    "Charlton Lesurf",
    "Kaine Gunston",
    "Arabel Kalinowsky",
    "Wynn Probet",
    "Goldarina Vasilchikov",
    "Wheeler Carmen",
    "Tandy Loader",
    "Fania McAvaddy",
    "Sonny Browncey",
    "Johny Chander",
    "Cristin Stennes",
    "Kary Whitta",
    "Tressa Imore",
    "Melany Woodruff",
    "Dre Iveson",
    "Danyette Cohn",
    "Samantha De Freitas"};
        string[] _costumerAddress = {"83712 Porter Drive",
    "3643 Doe Crossing Court",
    "7 Luster Street",
    "00722 Corry Center",
    "8075 Randy Place",
    "357 Di Loreto Park",
    "7 Village Point",
    "20910 Kipling Lane",
    "68 Pine View Point",
    "28715 Karstens Parkway",
    "755 Union Court",
    "79058 7th Crossing",
    "923 Vernon Way",
    "506 Pepper Wood Crossing",
    "59 Sloan Park",
    "26 Sommers Street",
    "436 Coleman Lane",
    "4180 Mcguire Drive",
    "303 Monterey Place",
    "2 Milwaukee Drive",
    "32 Myrtle Parkway",
    "9278 Brown Hill",
    "88711 Melby Alley",
    "85 Debra Way",
    "74022 Buena Vista Park",
    "9 Manitowish Pass",
    "99064 Comanche Drive",
    "016 Mallard Court",
    "5299 Mayfield Crossing",
    "8 Hazelcrest Hill",
    "0093 Rutledge Way",
    "6444 Loftsgordon Drive",
    "356 Anhalt Hill",
    "0 Fairview Alley",
    "5 Cascade Crossing",
    "48621 Mallory Plaza",
    "81 Ridge Oak Plaza",
    "95220 Hazelcrest Place",
    "2912 Cordelia Alley",
    "6407 Knutson Drive"};
        string[] _costumerEmail = {"jvaughan0@paginegialle.it",
    "gphalip1@tinypic.com",
    "klawrey2@sakura.ne.jp",
    "ktremblot3@alibaba.com",
    "hhabergham4@upenn.edu",
    "ewaterhouse5@mit.edu",
    "lmasser6@google.co.uk",
    "ddealmeida7@weibo.com",
    "kchinge8@disqus.com",
    "eosgorby9@gravatar.com",
    "evenablea@360.cn",
    "htimnyb@scientificamerican.com",
    "hlynaghc@salon.com",
    "dkarpenkod@nydailynews.com",
    "mbickerse@altervista.org",
    "lotleyf@go.com",
    "acampeg@adobe.com",
    "sklugeh@yahoo.com",
    "nmacconneelyi@statcounter.com",
    "shasseklj@nbcnews.com",
    "ejenicekk@deliciousdays.com",
    "dredfernl@simplemachines.org",
    "gmuellerm@samsung.com",
    "adurhamn@naver.com",
    "hridingso@com.com",
    "ssteerp@scientificamerican.com",
    "fsherbrookq@comcast.net",
    "lphinnisr@google.it",
    "mpoplees@opensource.org",
    "rleindeckert@miibeian.gov.cn",
    "rcomminsu@slideshare.net",
    "mpotierv@howstuffworks.com",
    "lrudledgew@nationalgeographic.com",
    "mwallicex@psu.edu",
    "hhardimany@nsw.gov.au",
    "jmackayz@woothemes.com",
    "tbavester10@netscape.com",
    "cmollett11@time.com",
    "adarko12@usgs.gov",
    "edunsford13@marriott.com" };

        DateTime _start = new DateTime(2021, 1, 1);
        int range = (DateTime.Now - _start).Days;

        for (int i = 0; i < 40; i++)
        {
            Order order = new Order
            {
                OrderID = Config._orderIDGet,
                CustomerName = _costumerName[i],
                CustomerEmail = _costumerEmail[i],
                CustomerAddress = _costumerAddress[i]
            };

            order.OrderDate = _start.AddDays(_random.Next(range - 30)).AddHours(_random.Next(0, 24))
                .AddMinutes(_random.Next(0, 60)).AddSeconds(_random.Next(0, 60));
            if (i < 32)
                order.ShipDate = order.OrderDate.Add(new TimeSpan(10, 0, 0, 0));
            else
                order.ShipDate = DateTime.MinValue;

            if (i < 24)
                order.DeliveryDate = order.ShipDate.Add(new TimeSpan(3, 0, 0, 0));
            else
                order.DeliveryDate = DateTime.MinValue;

            orders[Config._indexOrders++] = order;

        }

    }

    private static void initOrderItems()
    {
        for (int i = 0; i < 50; i++)
        {
            OrderItem orderItem = new OrderItem
            {
                OrderItemID = Config._orderItemIDGet,
                ProductID = products[_random.Next(0, 25)].ProductID,
                OrderID = orders[_random.Next(0, 40)].OrderID,
                Amount = _random.Next(1, 4)
            };
            for (int j = 0; j < 25; j++)
            {
                if (products[j].ProductID == orderItem.ProductID)
                    orderItem.Price = products[j].Price;
            }
            orderItems[Config._indexOrdersItems++] = orderItem;
        }
     
    }
}
