namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

internal class DalOrder : IOrder
{
    private string orderPath = @"Order";
    private string configIdPath = @"ConfigNumbers";
    private string? orderID = @"OrderID";

    //private List<Order?> orders = XMLTools.LoadListFromXMLSerializer<Order>(@"..\xml\Order.xml");
    /// <summary>
    /// Receives an order as a parameter and adds it to the array of orders
    /// </summary>
    /// <param name="addOrder"></param>
    /// <returns> Order ID number </returns>
    /// <exception cref="AddException"> if the array of orders are full </exception>
    public int Add(Order addOrder)
    {

        addOrder.OrderID = int.Parse(XElement.Load(configIdPath).Element(orderID!)!.Value) + 1;

        XElement.Load(configIdPath).Element(orderID!)!.SetValue(addOrder.OrderID);

        XElement orders = XMLTools.LoadListFromXMLElement(orderPath);

        XElement order = new XElement("Order", new XElement("OrderID", addOrder.OrderID),
                                               new XElement("CustomerName", addOrder.CustomerName),
                                               new XElement("CustomerEmail", addOrder.CustomerEmail),
                                               new XElement("CustomerAddress", addOrder.CustomerAddress),
                                               new XElement("OrderDate", addOrder.OrderDate),
                                               new XElement("ShipDate", addOrder.ShipDate),
                                               new XElement("DeliveryDate", addOrder.DeliveryDate));

        orders.Add(order);

        XMLTools.SaveListToXMLElement(orders, orderPath);

        return addOrder.OrderID;
    }

    /// <summary>
    /// Deleting the order from the array by deleting it and replacing it with the last place
    /// </summary>
    /// <param name="orderID"></param>
    /// <exception cref="NoFoundException"> if the order not exist </exception>
    public void Delete(int orderID)
    {
        XElement orders = XMLTools.LoadListFromXMLElement(orderPath);

        if (!orders.Elements().ToList().Exists(element => int.Parse(element.Element("OrderID")!.Value) == orderID))
            throw new NoFoundException("ORDER");

        XElement orderToDelete = (from item in orders.Elements()
                                  where int.Parse(item.Element("OrderID")!.Value) == orderID
                                  select item).FirstOrDefault()!;
        orderToDelete.Remove();

        XMLTools.SaveListToXMLElement(orders, orderPath);

    }

    /// <summary>
    /// Updating an order whose details have changed
    /// </summary>
    /// <param name="updateOrder"></param>  
    /// <exception cref="NoFoundException"> if the order not exist </exception>
    public void Update(Order updateOrder)
    {
        Delete(updateOrder.OrderID);

        XElement orders = XMLTools.LoadListFromXMLElement(orderPath);

        XElement order = new XElement("Order", new XElement("OrderID", updateOrder.OrderID),
                                               new XElement("CustomerName", updateOrder.CustomerName),
                                               new XElement("CustomerEmail", updateOrder.CustomerEmail),
                                               new XElement("CustomerAddress", updateOrder.CustomerAddress),
                                               new XElement("OrderDate", updateOrder.OrderDate),
                                               new XElement("ShipDate", updateOrder.ShipDate),
                                               new XElement("DeliveryDate", updateOrder.DeliveryDate));

        orders.Add(order);

        XMLTools.SaveListToXMLElement(orders, orderPath);
    }

    /// <summary>
    /// The function receives an ID number of an order
    /// and checks whether there is a matching order and returns the order
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns> Returns the requested order </returns>
    /// <exception cref="NoFoundException"> if the order not exist </exception>
    public Order Get(int orderID)
    {
        return Get(element => orderID == element?.OrderID);
    }

    /// <summary>
    /// The function receives an condition of an order
    /// and checks whether there is a matching order and returns the order
    /// </summary>
    /// <param name="func"></param>
    /// <returns> Returns the requested order </returns>
    /// <exception cref="NoFoundException"></exception>
    public Order Get(Func<Order?, bool>? func)
    {
        return GetAll(func).FirstOrDefault() ?? throw new NoFoundException("ORDER");
    }

    /// <summary>
    /// <returns>  Returns the order list in condition </returns>
    /// </summary>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func = null)
    {
        XElement elementRoot = XMLTools.LoadListFromXMLElement(orderPath);

        List<Order?> orders = (from item in elementRoot.Elements()
                               select (Order?)new Order
                               {
                                   OrderID = int.Parse(item.Element("OrderID")!.Value),
                                   CustomerAddress = item.Element("CustomerAddress")!.Value,
                                   CustomerEmail = item.Element("CustomerEmail")!.Value,
                                   CustomerName = item.Element("CustomerName")!.Value,
                                   OrderDate = Convert.ToDateTime(item.Element("OrderDate")!.Value),
                                   ShipDate = Convert.ToDateTime(item.Element("ShipDate")!.Value),
                                   DeliveryDate = Convert.ToDateTime(item.Element("DeliveryDate")!.Value)
                               }).ToList();

        if (func is null)
            return orders.Select(item => item);

        return orders.Where(func);
    }

}
