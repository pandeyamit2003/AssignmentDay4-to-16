using System;
public class Order
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public double Amount { get; set; }
}
// Services
public class EmailService
{
    public void SendEmail(Order order)
    {
        Console.WriteLine("Email sent to customer");
    }
}
public class SMSService
{
    public void SendSMS(Order order)
    {
        Console.WriteLine("SMS sent to customer");
    }
}
public class LoggerService
{
    public void LogOrder(Order order)
    {
        Console.WriteLine("Order logged successfully");
    }
}
// Order Processor with Delegate + Event
public class OrderProcessor
{
    // Delegate
    public delegate void OrderPlacedHandler(Order order);
    // Event
    public event OrderPlacedHandler OnOrderPlaced;
    public void ProcessOrder(Order order)
    {
        Console.WriteLine($"Order Placed: {order.OrderId}");
        // Trigger event (multicast delegate)
        OnOrderPlaced?.Invoke(order);
    }
}
class Program
{
    static void Main(string[] args)
    {
        // Create order
        Order order = new Order
        {
            OrderId = 101,
            CustomerName = "John",
            Amount = 2500
        };
        // Processor
        OrderProcessor processor = new OrderProcessor();
        // Services
        EmailService emailService = new EmailService();
        SMSService smsService = new SMSService();
        LoggerService loggerService = new LoggerService();
        // Subscribe (multicast)
        processor.OnOrderPlaced += emailService.SendEmail;
        processor.OnOrderPlaced += smsService.SendSMS;
        processor.OnOrderPlaced += loggerService.LogOrder;
        // Process order
        processor.ProcessOrder(order);
        Console.ReadLine();
    }
}
