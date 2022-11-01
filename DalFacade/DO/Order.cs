namespace DO;
public struct Order
{

    public int OrderID { get; set; }

    public string CustomerName { get; set; }

    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }

    public override string ToString() => $@"
        Product ID: {OrderID}
        Customer Name: {CustomerName}
        Customer Email: {CustomerEmail}
        Customer Address: {CustomerAddress}
        Order Date: {OrderDate}
        Ship Date: {ShipDate}
        Delivery Date: {DeliveryDate}";
}
