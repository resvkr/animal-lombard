using AnimalLombard.Modals;
using AnimalLombard.Services;
using AnimalLombard.Utils;

namespace AnimalLombard.View;

public class OrderView
{
    private readonly OrderService _orderService;
    
    public OrderView (OrderService orderService)
    {
        _orderService = orderService;
    }

    public void ShowOrder()
    {
        Console.WriteLine("Your current order: ");
        var orderDetails = _orderService.GetOrderDetails();
        Console.WriteLine(orderDetails);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public void PlaceOrder()
    {
        Console.WriteLine("Placing order...");
        var paymentType = EnumUtils.AskEnumValue<PaymentType>("Choose payment method: (Cash/Card)");

        var success = _orderService.PlaceOrder(paymentType);
        Console.WriteLine(success ? "Order placed successfully" : "Failed to place order");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public void ClearOrder()
    {
        Console.WriteLine("Do you want to clear your order? (y/N)");
        if (Console.ReadLine()?.Trim().Equals("y", StringComparison.OrdinalIgnoreCase) == true)
        {
            _orderService.ClearOrder();
            Console.WriteLine("Order cleared successfully");
        }
        else
        {
            Console.WriteLine("Order not cleared");
        }
    }
}