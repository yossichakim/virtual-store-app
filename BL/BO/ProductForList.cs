namespace BO;

/// <summary>
/// product for list
/// </summary>
public class ProductForList
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
    /// product price
    /// </summary>
    public double ProductPrice { get; set; }

    /// <summary>
    /// category
    /// </summary>
    public Category? Category { get; set; }

    /// <summary>
    /// print product for list
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"Product ID: {ProductID}\n" +
                                         $"Product Name: {ProductName}\n" +
                                         $"Product Price: {ProductPrice}\n" +
                                         $"Category: {Category}\n";
}