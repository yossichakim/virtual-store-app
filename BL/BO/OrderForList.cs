namespace BO;

internal class OrderForList
{
    public int OrderID { get; set; }
    public string CustomerName { get; set; }
    public OrderStatus Status { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }
}