namespace BO;

public class ProductForList
{
    public int ProductID { get; set; }
    public string? ProductName { get; set; }
    public double ProductPrice { get; set; }
    public Category Category { get; set; }
    public override string ToString() => $"Product ID: {ProductID}\n" +
                                         $"Product Name: {ProductName}\n" +
                                         $"Product Price: {ProductPrice}\n" +
                                         $"Category: {Category}\n";
}