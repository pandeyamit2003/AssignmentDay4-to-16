using System;
using System.Collections.Generic;
using System.Linq;
class Order
{
    public int OrderId;
    public string ProductName;
    public double Price;
    public string Category;
    public Stack<string> StatusHistory = new Stack<string>();
}
class Customer
{
    public int CustomerId;
    public string Name;
}
class OrderManagementSystem
{
    private List<Order> orders = new List<Order>();
    private Dictionary<int, Customer> customers = new Dictionary<int,Customer>();
    private HashSet<string> categories = new HashSet<string>();
    private Queue<Order> orderQueue = new Queue<Order>();
    
    // Add Customer
    public void AddCustomer(Customer customer)
    {
        customers[customer.CustomerId] = customer;
    }
    // Add Order
    public void AddOrder(Order order)
    {
        orders.Add(order);
        orderQueue.Enqueue(order);
        categories.Add(order.Category);
        order.StatusHistory.Push("Created");
    }
    // Update Order
    public void UpdateOrder(int orderId, string newProductName, double
    newPrice)
    {
        var order = orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order != null)
        {
            order.ProductName = newProductName;
            order.Price = newPrice;
            order.StatusHistory.Push("Updated");
        }
        else
        {
            Console.WriteLine("Order not found.");
        }
    }
    // Remove Order
    public void RemoveOrder(int orderId)
    {
        var order = orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order != null)
        {

            orders.Remove(order);
            order.StatusHistory.Push("Removed");
        }
        else
        {
            Console.WriteLine("Order not found.");
        }
    }
    // Process Orders (FIFO)
    public void ProcessNextOrder()
    {
        if (orderQueue.Count > 0)
        {
            var order = orderQueue.Dequeue();
            order.StatusHistory.Push("Processed");
            Console.WriteLine($"Processing Order ID: {order.OrderId}");
        }
        else
        {
            Console.WriteLine("No orders to process.");
        }
    }
    // View Status History
    public void PrintOrderStatus(int orderId)
    {
        var order = orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order != null)
        {
            Console.WriteLine($"\nStatus history for Order {orderId}:");
            foreach (var status in order.StatusHistory)
            {
                Console.WriteLine(status);
            }
        }
        else
        {
            Console.WriteLine("Order not found.");
        }
    }
    // Show Unique Categories
    public void PrintCategories()
    {
        Console.WriteLine("\nProduct Categories:");
        foreach (var category in categories)
        {
            Console.WriteLine(category);
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        var system = new OrderManagementSystem();
        // Add Customers
        system.AddCustomer(new Customer { CustomerId = 1, Name = "Alice" });
        system.AddCustomer(new Customer { CustomerId = 2, Name = "Bob" });
        // Add Orders
        system.AddOrder(new Order{OrderId = 101,ProductName = "Laptop",Price = 800,Category = "Electronics"});
        system.AddOrder(new Order{OrderId = 102,ProductName = "Shoes",Price = 50,Category = "Fashion"});
        system.AddOrder(new Order{OrderId = 103,ProductName = "Phone",Price = 500,Category = "Electronics"});
        // Update Order
        system.UpdateOrder(101, "Gaming Laptop", 1200);
        // Process Orders
        system.ProcessNextOrder();
        system.ProcessNextOrder();
        // Remove Order
        system.RemoveOrder(102);
        // Display Status
        system.PrintOrderStatus(101);
        system.PrintOrderStatus(102);
        // Display Categories
        system.PrintCategories();
        Console.WriteLine("\nExecution Completed.");
    }
}
