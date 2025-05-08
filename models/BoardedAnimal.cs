using System.ComponentModel.DataAnnotations;
using Utils;

namespace Models
{
    public class BoardedAnimal : Animal
    {
        [Required(ErrorMessage = "User ID is required.")]
        public string OwnerId { get; set; }

        [Required]
        public FeedingType FeedingType { get; set; }

        [Required(ErrorMessage = "Please enter the address information.")]
        public AddressInfo AddressInfo { get; set; }

        private BoardedAnimal(
            string name,
            AnimalType animalType,
            string species,
            string ownerId,
            FeedingType feedingType,
            string address,
            string city
        ) : base(name, animalType, species)
        {
            AddressInfo = AddressInfo.Create(address, city);
            OwnerId = ownerId;
            FeedingType = feedingType;
        }

        public override string ToString()
        {
            return $"BoardedAnimal: OwnerId: {OwnerId}, FeedingType: {FeedingType}, {AddressInfo} " + base.ToString();
        }

        public static BoardedAnimal Create(
            string name,
            AnimalType animalType,
            string species,
            string ownerId,
            FeedingType feedingType,
            string address,
            string city
        )
        {
            var boardedAnimal = new BoardedAnimal(name, animalType, species, ownerId, feedingType, address, city);
            ValidatorUtils.ValidateEntity(boardedAnimal);
            return boardedAnimal;
        }
    }
}