using System.ComponentModel.DataAnnotations;
using AnimalLombard.Utils;

namespace AnimalLombard.Modals;

public class Product
{
    private static long _nextId = 1;
    [Key] public string Id { get; private set; }

    [Required]
    [MinLength(10, ErrorMessage = "Name must be at least 10 characters long.")]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    [Range(0.01, 5000.00, ErrorMessage = "Price must be between 0.01 and 5000.00.")]
    public decimal Price { get; set; }

    private Product(string name, string description, decimal price)
    {
        Id = _nextId++.ToString();
        Name = name;
        Description = description;
        this.Price = price;
    }

    public override string ToString()
    {
        return $"Product: {Id}, Name: {Name}, Description: {Description}, Price: {Price}";
    }

    public static Product Create(string name, decimal price, string description = "")
    {
        var product = new Product(name, description, price);
        ValidatorUtils.ValidateEntity(product);
        return product;
    }
}