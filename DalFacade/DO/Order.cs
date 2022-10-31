namespace DO;
public struct Order {
    public int ProductID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }

        public override string ToString() => $@"
        Product ID: {ProductID}
        Customer Name: {CustomerName}
        Customer Email: {CustomerEmail}
        Customer Address: {CustomerAddress}
        Order Date: {OrderDate}
        Ship Date: {ShipDate}
        Delivery Date: {DeliveryDate}";
}
