using System.ComponentModel.DataAnnotations;

namespace Models
{
    class User
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; private set; }

        private User(string name)
        {
            Name = name;
        }

        public static User Create(string name)
        {
            var user = new User(name);
            user.Validate();
            return user;
        }

        private void Validate()
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(this, context, results, true);

            if (!isValid)
            {
                var errorMessages = string.Join("; ", results.Select(r => r.ErrorMessage));
                throw new ValidationException($"User validation failed: {errorMessages}");
            }
        }
    }
}