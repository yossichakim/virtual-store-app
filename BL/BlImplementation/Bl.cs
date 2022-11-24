using BLApi;

namespace BlImplementation;

public sealed class Bl : IBl
{
    public ICart Cart => new Cart();

    public IProduct Product => new Product();

    public IOrder Order => new Order();
}