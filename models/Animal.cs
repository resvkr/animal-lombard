using System.ComponentModel.DataAnnotations;

namespace Models
{
    public abstract class Animal
    {
        [Key]
        public string Id { get; private set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public AnimalType AnimalType { get; set; }

        [Required]
        public string Species { get; set; }

        protected Animal(string name, AnimalType animalType, string species)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            AnimalType = animalType;
            Species = species;
        }

        public override string ToString()
        {
            return $"Animal: {Name}, Type: {AnimalType}, Species: {Species}";
        }
    }
}