namespace BO;

public class OrderItem
{
    public int ProductID { get; set; }
    public string ProductName { get; set; } 
    public double ProductPrice { get; set; }
    public int Amount { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString() => $"Product ID: {ProductID}\n" +
                                         $"ProductName: {ProductName}\n"+
                                         $"Product Price: {ProductPrice}\n" +
                                         $"Amount: {Amount}\n" +
                                         $"Total Price: {TotalPrice}\n";
}