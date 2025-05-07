using System.ComponentModel.DataAnnotations;
using Utils;

namespace Models
{
    public class Order
    {
        [Key]
        public string Id { get; private set; }

        [Required]
        public User Customer { get; set; }

        [Required]
        public SaleAnimal Pet { get; set; }

        public List<Product>? AdditionalProducts { get; set; } = new List<Product>();

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public PaymentType PaymentType { get; set; }

        private Order(User customer, SaleAnimal pet, List<Product> additionalProducts, decimal totalPrice, PaymentType paymentType)
        {
            Id = Guid.NewGuid().ToString();
            Customer = customer;
            Pet = pet;
            AdditionalProducts = additionalProducts;
            TotalPrice = totalPrice;
            PaymentType = paymentType;
        }

        public override string ToString()
        {
            string additionalProducts = AdditionalProducts != null ? string.Join(", ", AdditionalProducts.Select(p => p.Name)) : "None";
            return $"Order: {Id}, Customer: {Customer}, Pet: {Pet}, Additional Products: {additionalProducts}, Total Price: {TotalPrice}, Payment Type: {PaymentType}";
        }

        public static Order Create(User customer, SaleAnimal pet, decimal totalPrice, PaymentType paymentType, List<Product>? additionalProducts = null)
        {
            var order = new Order(customer, pet, additionalProducts ?? new List<Product>(), totalPrice, paymentType);
            ValidatorUtils.ValidateEntity(order);
            return order;
        }
    }
}