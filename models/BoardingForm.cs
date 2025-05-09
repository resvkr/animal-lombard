using System.ComponentModel.DataAnnotations;
using Utils;

namespace Models
{
    public class BoardingForm
    {
        [Key]
        public string Id { get; private set; }

        [Required(ErrorMessage = "Please enter the name of the pet owner.")]
        public BoardedAnimal BoardedAnimal { get; set; }

        [Required(ErrorMessage = "Please enter the name of the pet.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please enter the name of the pet.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Please choose a payment method.")]
        public PaymentType PaymentType { get; set; }

        [Required(ErrorMessage = "Please enter the price of the boarding.")]
        public decimal Price { get; set; }


        private BoardingForm(BoardedAnimal boardedAnimal, DateTime startDate, DateTime endDate, PaymentType paymentType, decimal price)
        {
            Id = Guid.NewGuid().ToString();
            BoardedAnimal = boardedAnimal;
            StartDate = startDate;
            EndDate = endDate;
            PaymentType = paymentType;
            Price = price;
        }

        public override string ToString()
        {
            return $"BoardingForm: {Id}, BoardedAnimal: {BoardedAnimal}, StartDate: {StartDate}, EndDate: {EndDate}, PaymentType: {PaymentType}, Price: {Price}";
        }

        public static BoardingForm Create(BoardedAnimal boardedAnimal, DateTime startDate, DateTime endDate, PaymentType paymentType, decimal price)
        {
            var boardingForm = new BoardingForm(boardedAnimal, startDate, endDate, paymentType, price);
            ValidatorUtils.ValidateEntity(boardingForm);
            return boardingForm;
        }
    }
}