namespace BO;

public class OrderItem
{
    public int OrderItemID { get; set; }

    public int ProductID { get; set; }

    public double ProductPrice { get; set; }

    public int Amount { get; set; }

    public double TotalPrice { get; set; }

    public override string ToString() => $"Order Item ID: {OrderItemID}\n" +
                                         $"Product ID: {ProductID}\n" +
                                         $"Product Price: {ProductPrice}\n" +
                                         $"Amount: {Amount}\n"+
                                         $"Total Price: {TotalPrice}\n";
                                      
}