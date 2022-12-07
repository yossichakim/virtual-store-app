namespace DalApi;

/// <summary>
/// General interface for the data entities
/// </summary>
public interface IDal
{
    /// <summary>
    /// interface for a product
    /// </summary>
    IProduct Product { get; }

    /// <summary>
    /// Interface for order
    /// </summary>
    IOrder Order { get; }

    /// <summary>
    /// Interface for order item
    /// </summary>
    IOrderItem OrderItem { get; }
}