namespace BO;

public class Product
{
    public int ProductID { get; set; }
    public string? ProductName { get; set; }
    public double ProductPrice { get; set; }
    public Category Categoty { get; set; }
    public int InStock { get; set; }
    public override string ToString() => $"Product ID: {ProductID}\n" +
                                         $"Product Name: {ProductName}\n" +
                                         $"Product Price: {ProductPrice}\n" +
                                         $"Categoty: {Categoty}\n" +
                                         $"Amount in stock: {InStock}\n";
}