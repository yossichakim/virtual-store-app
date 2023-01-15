namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

/// <summary>
/// Access to the data of order items with the possibility of changes
/// </summary>
internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// The main path of order items
    /// </summary>
    private string orderItemPath = @"OrderItem";

    /// <summary>
    /// The main path of the config numbers
    /// </summary>
    private string configIdPath = @"..\xml\ConfigNumbers.xml";
    /// <summary>
    /// the path of order Item ID
    /// </summary>
    private string orderItemID = @"OrderItemID";

    /// <summary>
    /// Holds the data of order items
    /// </summary>
    private XElement orderItems;

    /// <summary>
    /// Adding order items to the list of order items
    /// </summary>
    /// <param name="addOrderItem"></param>
    /// <returns></returns>
    /// <exception cref="AddException"> if the array of orders items are full </exception>
    public int Add(OrderItem addOrderItem)
    {

        //initialize Running ID number
        addOrderItem.OrderItemID = int.Parse(XElement.Load(configIdPath).Element(orderItemID!)!.Value) + 1;

        XDocument configId = XDocument.Load(configIdPath);

        configId.Descendants(orderItemID).FirstOrDefault()!.Value = addOrderItem.OrderItemID.ToString();

        configId.Save(configIdPath);

        orderItems = XMLTools.LoadListFromXMLElement(orderItemPath);

        XElement orderItem = new XElement("OrderItem", new XElement("OrderItemID", addOrderItem.OrderItemID),
                                                        new XElement("ProductID", addOrderItem.ProductID),
                                                        new XElement("OrderID", addOrderItem.OrderID),
                                                        new XElement("Price", addOrderItem.Price),
                                                        new XElement("Amount", addOrderItem.Amount));

        orderItems.Add(orderItem);

        XMLTools.SaveListToXMLElement(orderItems, orderItemPath);

        return addOrderItem.OrderItemID;
    }

    /// <summary>
    /// Deletion of an order item by ID number of the order item
    /// </summary>
    /// <param name="orderItemID"></param>
    /// <exception cref="NoFoundException"> if the order item not exist </exception>
    public void Delete(int orderItemID)
    {

        orderItems = XMLTools.LoadListFromXMLElement(orderItemPath);

        if (!orderItems.Elements().ToList().Exists(element => int.Parse(element.Element("OrderItemID")!.Value) == orderItemID))
            throw new NoFoundException("ORDER ITEM");

        XElement orderItemToDelete = (from item in orderItems.Elements()
                                      where int.Parse(item.Element("OrderItemID")!.Value) == orderItemID
                                      select item).FirstOrDefault()!;
        orderItemToDelete.Remove();

        XMLTools.SaveListToXMLElement(orderItems, orderItemPath);

    }

    /// <summary>
    /// Order item update of a requested product
    /// </summary>
    /// <param name="updateOrderItem"></param>
    /// <exception cref="NoFoundException"> if the order item not exist </exception>
    public void Update(OrderItem updateOrderItem)
    {
        Delete(updateOrderItem.OrderItemID);

        orderItems = XMLTools.LoadListFromXMLElement(orderItemPath);

        XElement orderItem = new XElement("OrderItem", new XElement("ProductID", updateOrderItem.OrderItemID),
                                                        new XElement("ProductID", updateOrderItem.ProductID),
                                                        new XElement("OrderID", updateOrderItem.OrderID),
                                                        new XElement("Price", updateOrderItem.Price),
                                                        new XElement("Amount", updateOrderItem.Amount));

        orderItems.Add(orderItem);

        XMLTools.SaveListToXMLElement(orderItems, orderItemPath);

    }

    /// <summary>
    /// Find the order items by the ID number
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns> Returns the requested order item </returns>
    /// <exception cref="NoFoundException"> if the order item not exist </exception>
    public OrderItem Get(int orderItem)
    {
        return Get(element => element?.OrderItemID == orderItem);
    }

    /// <summary>
    /// The function receives an condition of an order item
    /// and checks whether there is a matching order item and returns the order item
    /// </summary>
    /// <param name="func"></param>
    /// <returns> Returns the requested order item </returns>
    /// <exception cref="NotImplementedException"></exception>
    public OrderItem Get(Func<OrderItem?, bool>? func)
    {
        return GetAll(func).FirstOrDefault() ?? throw new NoFoundException("ORDER ITEM");
    }

    /// <summary>
    /// <returns> Returns the list of order items in condition </returns>
    /// </summary>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func = null)
    {
        orderItems = XMLTools.LoadListFromXMLElement(orderItemPath);

        List<OrderItem?> orderItemsList = (from item in orderItems.Elements()
                                           select (OrderItem?)new OrderItem
                                           {
                                               OrderItemID = int.Parse(item.Element("OrderItemID")!.Value),
                                               OrderID = int.Parse(item.Element("OrderID")!.Value),
                                               ProductID = int.Parse(item.Element("ProductID")!.Value),
                                               Amount = int.Parse(item.Element("Amount")!.Value),
                                               Price = double.Parse(item.Element("Price")!.Value)
                                           }).ToList();

        if (func is null)
            return orderItemsList.Select(item => item);

        return orderItemsList.Where(func);
    }
}
