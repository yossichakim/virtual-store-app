namespace BLApi;

public interface IBl
{
    public ICart Cart { get; }
    public IProduct Product { get; }
    public IOrder Order { get; }
}