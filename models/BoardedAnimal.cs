using System.ComponentModel.DataAnnotations;
using Utils;

namespace Models
{
    public class BoardedAnimal : Animal
    {
        [Required]
        public User Owner { get; set; }

        [Required]
        public FeedingType FeedingType { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }


        // Можливо варто переробити так щоб Address і City були як клас InfoAddress

        private BoardedAnimal(
            string name,
            AnimalType animalType,
            string species,
            User owner,
            FeedingType feedingType,
            string address,
            string city
        ) : base(name, animalType, species)
        {
            Owner = owner;
            FeedingType = feedingType;
            Address = address;
            City = city;
        }

        public override string ToString()
        {
            return $"BoardedAnimal: Owner: {Owner.ToString}, FeedingType: {FeedingType}, Address: {Address}, City: {City} " + base.ToString();
        }

        public static BoardedAnimal Create(
            string name,
            AnimalType animalType,
            string species,
            User owner,
            FeedingType feedingType,
            string address,
            string city
        )
        {
            var boardedAnimal = new BoardedAnimal(name, animalType, species, owner, feedingType, address, city);
            ValidatorUtils.ValidateEntity(boardedAnimal);
            return boardedAnimal;
        }
    }
}