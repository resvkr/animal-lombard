using AnimalLombard.Context.Interfaces;
using AnimalLombard.Modals;

namespace AnimalLombard.Context;

public class OrderContext : IOrderContext
{
    private readonly IReadOnlyUserContext _userContext;
    private readonly Dictionary<string, Order> _orders = new();
    private Order? _tempOrder;

    public Order? CurrentOrder =>
        _userContext.CurrentUser is { Id: var userId }
            ? _orders.GetValueOrDefault(userId)
            : _tempOrder;


    public OrderContext(IReadOnlyUserContext userContext)
    {
        _userContext = userContext;
    }

    public void StartNewOrder()
    {
        var userId = GetCurrentUserIdOrNull();

        if (CurrentOrder is not null)
            throw new InvalidOperationException("User already has an active order");


        if (userId is null)
        {
            var order = Order.Create();
            _tempOrder = order;
        }
        else
        {
            var order = Order.Create(_userContext.CurrentUser!);
            _orders.Add(userId, order);
        }
    }

    public void AddSaleAnimalToOrder(SaleAnimal saleAnimal)
    {
        var order = CurrentOrder;
        if (order is null)
            throw new InvalidOperationException("User does not have an active order");
        if (order.Pet is not null)
            throw new InvalidOperationException("Order already has a pet, you need to finalize or clear the order");

        order.Pet = saleAnimal;
        order.TotalPrice += saleAnimal.Price;
    }

    public void AddProductToOrder(Product product)
    {
        var order = CurrentOrder;

        if (order is null)
            throw new InvalidOperationException("User does not have an active order");
        
        if (order.AdditionalProducts is null)
            order.AdditionalProducts = new List<Product> { product };
        else
            order.AdditionalProducts.Add(product);

        order.TotalPrice += product.Price;
    }

    public void SetPaymentType(PaymentType paymentType)
    {
        var order = CurrentOrder;

        if (order is null)
            throw new InvalidOperationException("User does not have an active order");

        order.PaymentType = paymentType;
    }

    public void FinalizeOrder()
    {
        var order = CurrentOrder;

        if (order is null)
            throw new InvalidOperationException("User does not have an active order");

        var orderLog = DateTime.UtcNow + " - Order finalized: " + order;

        using var outputFile = new StreamWriter("order_log.txt", append: true);
        outputFile.WriteLine(orderLog);
        Clear();
    }

    public void Clear()
    {
        var userId = GetCurrentUserIdOrNull();

        if (userId is null)
        {
            if (_tempOrder is null)
                throw new InvalidOperationException("User does not have an active order");

            _tempOrder = null;
        }
        else
        {
            if (!_orders.ContainsKey(userId))
                throw new InvalidOperationException("User does not have an active order");

            _orders.Remove(userId);
        }
    }

    private string? GetCurrentUserIdOrNull()
    {
        return _userContext.CurrentUser?.Id ?? null;
    }
}