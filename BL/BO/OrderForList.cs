namespace BO;

internal class OrderForList
{
    public int OrderID { get; set; }
    public string? CustomerName { get; set; }
    public OrderStatus Status { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $"OrderID: {OrderID}\n" +
                                         $"Customer Name: {CustomerName}\n" +
                                         $"Status: {Status}\n" +
                                         $"Amount Of Items: {AmountOfItems}\n" +
                                         $"Total Price: {TotalPrice}\n";
}