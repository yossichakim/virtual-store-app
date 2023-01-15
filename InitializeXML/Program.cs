namespace Dal;
using DalApi;
using DO;

/// <summary>
/// Initialization of XML files in the data
/// </summary>
internal class Program
{
    /// <summary>
    /// Access data using DalApi
    /// </summary>
    private static IDal dallist = Factory.Get()!;

    static void Main(string[] args)
    {
        string productPath = @"Product";
        string orderPath = @"Order";
        string orderItemPath = @"OrderItem";


        List<Product?> products = dallist.Product.GetAll().ToList();
        List<Order?> orders = dallist.Order.GetAll().ToList();
        List<OrderItem?> orderItems = dallist.OrderItem.GetAll().ToList();

        XMLTools.SaveListToXMLSerializer(products, productPath);
        XMLTools.SaveListToXMLSerializer(orders, orderPath);
        XMLTools.SaveListToXMLSerializer(orderItems, orderItemPath);
    }
}
