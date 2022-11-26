namespace BLApi;

/// <summary>
/// General interface to the logical layer
/// </summary>
public interface IBl
{
    /// <summary>
    /// Cart entity interface
    /// </summary>
    public ICart Cart { get; }

    /// <summary>
    /// Product entity interface
    /// </summary
    public IProduct Product { get; }

    /// <summary>
    /// Order entity interface
    /// </summary
    public IOrder Order { get; }
}