// See https://aka.ms/new-console-template for more information
using DO;

Console.WriteLine("Hello, World!");
Order Order = new Order();
Order.OrderID = 234;

Order.CustomerName = "david";


Product Product1 = new Product { ProductID = 345, Name = "galksy10" };



Product Product2 = new Product { ProductID = 456, Name = "galksy8" };

OrderItem OrderItem1 = new OrderItem { OrderItemID = 1, OrderID = 234, ProductID = 456, Amount = 5, Price = 300 };


OrderItem OrderItem2 = new OrderItem { OrderID = 234, ProductID = 345, Amount = 2, Price = 300 };

