namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

/// <summary>
/// access to data of Order
/// </summary>
internal class DalOrder : IOrder
{
    /// <summary>
    /// The main path of order
    /// </summary>
    private string orderPath = @"Order";
    /// <summary>
    /// The main path of config numbers
    /// </summary>
    private string configIdPath = @"..\xml\ConfigNumbers.xml";

    /// <summary>
    /// the path of order ID
    /// </summary>
    private string? orderID = @"OrderID";

    /// <summary>
    /// List of order
    /// </summary>
    List<Order?>? orders;
    /// <summary>
    /// Receives an order as a parameter and adds it to the array of orders
    /// </summary>
    /// <param name="addOrder"></param>
    /// <returns> Order ID number </returns>
    /// <exception cref="AddException"> if the array of orders are full </exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Order addOrder)
    {
        orders = XMLTools.LoadListFromXMLSerializer<Order>(orderPath);

        if (orders.Exists(element => element?.OrderID == addOrder.OrderID))
            throw new AddException("ORDER");

        //initialize Running ID number
        addOrder.OrderID = int.Parse(XElement.Load(configIdPath).Element(orderID!)!.Value) + 1;

        XDocument configId = XDocument.Load(configIdPath);

        configId.Descendants(orderID).FirstOrDefault()!.Value = addOrder.OrderID.ToString();

        configId.Save(configIdPath);

        orders.Add(addOrder);

        XMLTools.SaveListToXMLSerializer(orders, orderPath);

        return addOrder.OrderID;
    }

    /// <summary>
    /// Deleting the order from the array by deleting it and replacing it with the last place
    /// </summary>
    /// <param name="orderID"></param>
    /// <exception cref="NoFoundException"> if the order not exist </exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int orderID)
    {
        orders = XMLTools.LoadListFromXMLSerializer<Order>(orderPath);

        if (!orders.Exists(element => element?.OrderID == orderID))
            throw new NoFoundException("ORDER");

        orders.RemoveAll(element => element?.OrderID == orderID);

        XMLTools.SaveListToXMLSerializer(orders, orderPath);

    }

    /// <summary>
    /// Updating an order whose details have changed
    /// </summary>
    /// <param name="updateOrder"></param>
    /// <exception cref="NoFoundException"> if the order not exist </exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Order updateOrder)
    {
        orders = XMLTools.LoadListFromXMLSerializer<Order>(orderPath);

        Delete(updateOrder.OrderID);
        orders.Add(updateOrder);

        XMLTools.SaveListToXMLSerializer(orders, orderPath);

    }

    /// <summary>
    /// The function receives an ID number of an order
    /// and checks whether there is a matching order and returns the order
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns> Returns the requested order </returns>
    /// <exception cref="NoFoundException"> if the order not exist </exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Get(Func<Order?, bool>? func)
    {
        orders = XMLTools.LoadListFromXMLSerializer<Order>(orderPath);

        if (orders.FirstOrDefault(func!) is Order order)
            return order;

        throw new NoFoundException("ORDER");
    }

    /// <summary>
    /// <returns>  Returns the order list in condition </returns>
    /// </summary>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func = null)
    {
        orders = XMLTools.LoadListFromXMLSerializer<Order>(orderPath);

        if (func is null)
            return orders.Select(item => item);

        return orders.Where(func);
    }

}
