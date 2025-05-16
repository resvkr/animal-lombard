using System.ComponentModel.DataAnnotations;
using AnimalLombard.Utils;

namespace AnimalLombard.Modals;

public class Order
{
    private static long _nextId = 1;
    [Key] public string Id { get; private set; }

    public User? Customer { get; set; }

    public SaleAnimal? Pet { get; set; }

    public List<Product>? AdditionalProducts { get; set; }

    public decimal? TotalPrice { get; set; }

    public PaymentType? PaymentType { get; set; }
    
    public DateTime? CreatedAt { get; set; }

    private Order(User customer, SaleAnimal pet, List<Product> additionalProducts, decimal totalPrice,
        PaymentType paymentType)
    {
        Id = _nextId++.ToString();
        Customer = customer;
        Pet = pet;
        AdditionalProducts = additionalProducts;
        TotalPrice = totalPrice;
        PaymentType = paymentType;
        CreatedAt = DateTime.UtcNow;    
    }

    private Order(User customer)
    {
        Id = _nextId++.ToString();
        Customer = customer;
        Pet = null;
        AdditionalProducts = null;
        TotalPrice = 0;
        PaymentType = null;
        CreatedAt = DateTime.UtcNow;
    }
    
    private Order()
    {
        Id = _nextId++.ToString();
        Customer = null;
        Pet = null;
        AdditionalProducts = null;
        TotalPrice = 0;
        PaymentType = null;
        CreatedAt = DateTime.UtcNow;
    }

    public override string ToString()
    {
        var additionalProducts = AdditionalProducts != null
            ? string.Join(", ", AdditionalProducts.Select(p => p.Name))
            : "None";
        return
            $"Order: {Id}, Customer: {Customer}, Pet: {Pet}, Additional Products: {additionalProducts}, Total Price: {TotalPrice}, Payment Type: {PaymentType.ToString()}, Created At: {CreatedAt}";
    }

    public static Order Create(User customer, SaleAnimal pet, decimal totalPrice, PaymentType paymentType,
        List<Product>? additionalProducts = null)
    {
        var order = new Order(customer, pet, additionalProducts ?? new List<Product>(), totalPrice, paymentType);
        ValidatorUtils.ValidateEntity(order);
        return order;
    }
    
    public static Order Create()
    {
        var order = new Order();
        ValidatorUtils.ValidateEntity(order);
        return order;
    }

    public static Order Create(User customer)
    {
        var order = new Order(customer);
        ValidatorUtils.ValidateEntity(order);
        return order;
    }
}