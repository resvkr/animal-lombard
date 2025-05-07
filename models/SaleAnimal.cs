using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Utils;

namespace Models
{
    public class SaleAnimal : Animal
    {
        public string? Description { get; set; }

        [Required]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public bool isAvailable { get; set; }

        public Dictionary<string, string>? Attributes { get; set; }

        private SaleAnimal(
            string name,
            AnimalType animalType,
            string species,
            string description,
            decimal price,
            bool isAvailable,
            Dictionary<string, string> attributes
            ) : base(name, animalType, species)
        {
            Description = description;
            Price = price;
            this.isAvailable = isAvailable;
            Attributes = attributes ?? new Dictionary<string, string>();
        }

        public override string ToString()
        {
            string jsonAttributes = JsonSerializer.Serialize(Attributes);
            return $"SaleAnimal: {Description}, Price: {Price}, Available: {isAvailable}, Attributes: {jsonAttributes} " + base.ToString();
        }

        public static SaleAnimal Create(
            string name,
            AnimalType animalType,
            string species,
            string description = "",
            decimal price = 0,
            bool isAvailable = true,
            Dictionary<string, string>? attributes = null
        )
        {
            attributes ??= new Dictionary<string, string>();
            var saleAnimal = new SaleAnimal(name, animalType, species, description, price, isAvailable, attributes);
            ValidatorUtils.ValidateEntity(saleAnimal);
            return saleAnimal;
        }
    }
}