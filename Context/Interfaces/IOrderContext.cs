using AnimalLombard.Modals;

namespace AnimalLombard.Context.Interfaces;

public interface IOrderContext
{
    Order? CurrentOrder { get; }
    void StartNewOrder();
    void AddSaleAnimalToOrder(SaleAnimal saleAnimal);
    void AddProductToOrder(Product product);
    void SetPaymentType(PaymentType paymentType);
    void FinalizeOrder();
    void Clear();
}