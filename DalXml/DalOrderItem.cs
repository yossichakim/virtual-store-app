namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

internal class DalOrderItem : IOrderItem
{
    private string orderItemPath = @"..\xml\OrderItem.xml";
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
        List<OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(orderItemPath);

        //initialize Running ID number
        addOrderItem.OrderItemID = int.Parse(XElement.Load(configIdPath).Element(OrderItemID!)!.Value) + 1;
        orderItems.Add(addOrderItem);

        XElement.Load(configIdPath).Element(OrderItemID!)!.SetValue(addOrderItem.OrderItemID);

        XMLTools.SaveListToXMLSerializer(orderItems, orderItemPath);
        return addOrderItem.OrderItemID;
    }

    /// <summary>
    /// Deletion of an order item by ID number of the order item
    /// </summary>
    /// <param name="orderItemID"></param>
    /// <exception cref="NoFoundException"> if the order item not exist </exception>
    public void Delete(int orderItemID)
    {
        List<OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(orderItemPath);

        if (!orderItems.Exists(element => element?.OrderItemID == orderItemID))
            throw new NoFoundException("ORDER ITEM");

        orderItems.RemoveAll(element => element?.OrderItemID == orderItemID);
        XMLTools.SaveListToXMLSerializer(orderItems, orderItemPath);

    }

    /// <summary>
    /// Order item update of a requested product
    /// </summary>
    /// <param name="updateOrderItem"></param>
    /// <exception cref="NoFoundException"> if the order item not exist </exception>
    public void Update(OrderItem updateOrderItem)
    {
        List<OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(orderItemPath);

        Delete(updateOrderItem.OrderItemID);
        orderItems.Add(updateOrderItem);

        XMLTools.SaveListToXMLSerializer(orderItems, orderItemPath);

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
        List<OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(orderItemPath);

        if (orderItems.FirstOrDefault(func!) is OrderItem orderItem)
            return orderItem;

        throw new NoFoundException("ORDER ITEM");
    }

    /// <summary>
    /// <returns> Returns the list of order items in condition </returns>
    /// </summary>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func = null)
    {
        List<OrderItem?> orderItems = XMLTools.LoadListFromXMLSerializer<OrderItem>(orderItemPath);

        if (func is null)
            return orderItems.Select(item => item);

         return orderItems.Where(func);
    }
}
