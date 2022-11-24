using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem>
{
    public OrderItem Find(int productID, int orderID);

    public IEnumerable<OrderItem> GetByOrderID(int orderID);
}