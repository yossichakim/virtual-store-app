using BLApi;

namespace BlImplementation;

/// <summary>
/// Access class to the interfaces of the logical layer
/// </summary>
internal sealed class Bl : IBl
{
    /// <summary>
    /// Cart entity interface
    /// </summary>
    public ICart Cart => new Cart();

    /// <summary>
    /// Product entity interface
    /// </summary>
    public IProduct Product => new Product();

    /// <summary>
    /// Order entity interface
    /// </summary>
    public IOrder Order => new Order();
}