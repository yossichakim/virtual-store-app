namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class DalOrderItem : IOrderItem
{
    private string orderItemPath = @"OrderItem";
    private string configIdPath = @"..\xml\ConfigNumbers.xml";
    private string OrderItemID = @"OrderItemID";

    /// <summary>
    /// Adding order items to the list of order items
    /// </summary>
    /// <param name="addOrderItem"></param>
    /// <returns></returns>
    /// <exception cref="AddException"> if the array of orders items are full </exception>
    public int Add(OrderItem addOrderItem)
    {

        //initialize Running ID number
        addOrderItem.OrderItemID = int.Parse(XElement.Load(configIdPath).Element(OrderItemID!)!.Value) + 1;

        XElement.Load(configIdPath).Element(OrderItemID!)!.SetValue(addOrderItem.OrderItemID);

        XElement element = XMLTools.LoadListFromXMLElement(orderItemPath);

        XElement orderItems = new XElement("OrderItem", new XElement("OrderItemID", addOrderItem.OrderItemID),
                                                        new XElement("ProductID", addOrderItem.ProductID),
                                                        new XElement("OrderID", addOrderItem.OrderID),
                                                        new XElement("Price", addOrderItem.Price),
                                                        new XElement("Amount", addOrderItem.Amount));

        element.Add(orderItems);

        XMLTools.SaveListToXMLElement(element, orderItemPath);

        return addOrderItem.OrderItemID;
    }

    /// <summary>
    /// Deletion of an order item by ID number of the order item
    /// </summary>
    /// <param name="orderItemID"></param>
    /// <exception cref="NoFoundException"> if the order item not exist </exception>
    public void Delete(int orderItemID)
    {

        XElement element = XMLTools.LoadListFromXMLElement(orderItemPath);

        if (!element.Elements().ToList().Exists(element => int.Parse(element.Element("OrderItemID")!.Value) == orderItemID))
            throw new NoFoundException("ORDER ITEM");

        XElement orderItemToDelete = (from item in element.Elements()
                                      where int.Parse(item.Element("OrderItemID")!.Value) == orderItemID
                                      select item).FirstOrDefault()!;
        orderItemToDelete.Remove();

        XMLTools.SaveListToXMLElement(element, orderItemPath);

    }

    /// <summary>
    /// Order item update of a requested product
    /// </summary>
    /// <param name="updateOrderItem"></param>
    /// <exception cref="NoFoundException"> if the order item not exist </exception>
    public void Update(OrderItem updateOrderItem)
    {
        Delete(updateOrderItem.OrderItemID);

        XElement element = XMLTools.LoadListFromXMLElement(orderItemPath);

        XElement orderItems = new XElement("OrderItem", new XElement("ProductID", updateOrderItem.OrderItemID),
                                                        new XElement("ProductID", updateOrderItem.ProductID),
                                                        new XElement("OrderID", updateOrderItem.OrderID),
                                                        new XElement("Price", updateOrderItem.Price),
                                                        new XElement("Amount", updateOrderItem.Amount));

        element.Add(orderItems);

        XMLTools.SaveListToXMLElement(element, orderItemPath);

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
        XElement elementRoot = XMLTools.LoadListFromXMLElement(orderItemPath);

        List<OrderItem?> orderItems = (from item in elementRoot.Elements()
                                       select (OrderItem?)new OrderItem
                                       {
                                           OrderItemID = int.Parse(item.Element("OrderItemID")!.Value),
                                           OrderID = int.Parse(item.Element("OrderID")!.Value),
                                           ProductID = int.Parse(item.Element("ProductID")!.Value),
                                           Amount = int.Parse(item.Element("Amount")!.Value),
                                           Price = double.Parse(item.Element("Price")!.Value)
                                       }).ToList();

        if (func is null)
            return orderItems.Select(item => item);

        return orderItems.Where(func);
    }
}
