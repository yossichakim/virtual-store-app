using Dal;
using DO;

internal class Program
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

    private DalProduct _dalProduct = new DalProduct();
    private DalOrder _dalOrder = new DalOrder();
    private DalOrderItem _dalOrderItem = new DalOrderItem();

    static void Main(string[] args)
    {
        Console.WriteLine("enter your choice:");
        Console.WriteLine("1 - Product Menu");
        Console.WriteLine("2 - Order Menu");
        Console.WriteLine("3 - Order Item Menu");
        Console.WriteLine("0 - Finish the program");

        int choice = Console.Read();

        switch (choice)
        {
            case 0:
                return;
            case 1:
                ProductMenu();

                break;
            //case 1: ;
            default:
                break;
        }



    }
    void ProductMenu()
    {
        Product product = new Product();
        Console.WriteLine("enter your choice:");
        int choice = Console.Read();

        switch (choice)
        {
            case 0:
                _dalProduct.AddProduct(SetProd(product));

            default:
                break;
        }
    }

    Product SetProd(Product product)
    {
        Console.WriteLine("enter product id:");
        product.ProductID = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter product name:");
        product.Name = Console.ReadLine();
        Console.WriteLine("enter product category:");
        product.Category = (Category)Console.Read();
        Console.WriteLine("enter product price:");
        product.Price = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("enter product stock:");
        product.InStock = Convert.ToInt32(Console.ReadLine());

        return product;
    }

    Order SetOrder(Order order)
    {
        Console.WriteLine("enter order id:");
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
}
