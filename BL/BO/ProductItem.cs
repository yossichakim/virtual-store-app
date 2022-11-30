namespace BO;

/// <summary>
/// product item
/// </summary>
public class ProductItem
{
    /// <summary>
    /// product ID
    /// </summary>
    public int ProductID { get; set; }

    /// <summary>
    /// product name
    /// </summary>
    public string? ProductName { get; set; }

    /// <summary>
    /// ProductPrice
    /// </summary>
    public double ProductPrice { get; set; }

    /// <summary>
    /// categoty
    /// </summary>
    public Category? Categoty { get; set; }

    /// <summary>
    /// amount in cart
    /// </summary>
    public int AmountInCart { get; set; }

    /// <summary>
    /// in stock
    /// </summary>
    public bool InStock { get; set; }

    /// <summary>
    /// print product item
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"Product ID: {ProductID}\n" +
                                         $"Product Name: {ProductName}\n" +
                                         $"Product Price: {ProductPrice}\n" +
                                         $"Category: {Categoty}\n" +
                                         $"Amount In Cart: {AmountInCart}\n" +
                                         $"Amount in stock: {InStock}\n";
}