namespace BO;

public class ProductItem
{
    public int ProductID { get; set; }
    public string? ProductName { get; set; }
    public double ProductPrice { get; set; }
    public Category Categoty { get; set; }
    public int AmountInCart { get; set; }
    public bool InStock { get; set; }

    public override string ToString() => $"Product ID: {ProductID}\n" +
                                         $"Product Name: {ProductName}\n" +
                                         $"Product Price: {ProductPrice}\n" +
                                         $"Category: {Categoty}\n" +
                                         $"Amount In Cart: {AmountInCart}\n" +
                                         $"Amount in stock: {InStock}\n";
}