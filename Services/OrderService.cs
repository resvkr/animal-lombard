using AnimalLombard.Context.Interfaces;
using AnimalLombard.Modals;
using AnimalLombard.Repository;
using AnimalLombard.Repository.Interfaces;

namespace AnimalLombard.Services;

public class OrderService
{
    private readonly IOrderContext _orderContext;
    private readonly IOrderRepository _orderRepository;

    public OrderService(DataStore dataStore, IOrderContext orderContext)
    {
        _orderRepository = dataStore.OrderRepository;
        _orderContext = orderContext;
    }

    public string GetOrderDetails()
    {
        var order = _orderContext.CurrentOrder;

        return order is null ? "No active order" : order.ToString();
    }

    public bool PlaceOrder(PaymentType paymentType)
    {
        try
        {
            _orderContext.SetPaymentType(paymentType);
            var order = _orderContext.CurrentOrder;
            if (order is null)
                throw new InvalidOperationException("Order is null.");
            _orderRepository.Save(order);
            _orderContext.FinalizeOrder();
            return true;
        } catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public void ClearOrder()
    {
        try
        {
            _orderContext.Clear();
        } catch (InvalidOperationException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}