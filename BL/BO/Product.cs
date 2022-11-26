namespace BO;

/// <summary>
/// product
/// </summary>
public class Product
{
    /// <summary>
    /// ProductID
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// product name
    /// </summary>
    public string? ProductName { get; set; }

    /// <summary>
    /// product price
    /// </summary>
    public double ProductPrice { get; set; }

    /// <summary>
    /// category
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// in stock
    /// </summary>
    public int InStock { get; set; }

    /// <summary>
    /// print product
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"Product ID: {ProductID}\n" +
                                         $"Product Name: {ProductName}\n" +
                                         $"Product Price: {ProductPrice}\n" +
                                         $"Category: {Category}\n" +
                                         $"Amount in stock: {InStock}\n";
}