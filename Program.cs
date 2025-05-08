using System.ComponentModel.DataAnnotations;
using Models;
using Utils;

class Program
{
    public static void Main()
    {
        string password = "SuperSecretPassword123!";

        var hashedPassword = HashUtils.HashPassword(password);

        var isValid = HashUtils.VerifyPassword(password, hashedPassword);
        var isValid2 = HashUtils.VerifyPassword("notSecretPassword", hashedPassword);

        Console.WriteLine($"Password: {password}");
        Console.WriteLine($"Hashed Password: {hashedPassword}");
        Console.WriteLine($"Is Valid: {isValid}");
        Console.WriteLine($"Is Valid 2: {isValid2}");

        var user = User.Create("John Doe", "+48693201532", "nikita.lisyuk06@gmail.com", "SuperSecretPassword123!");
        Console.WriteLine(user.ToString());
        try
        {
            var user2 = User.Create("John Doe", "+486", "nikita.lisyuk06@gmail.com", "SuperSecretPassword123!");
        }
        catch (ValidationException ex)
        {
            Console.WriteLine("Should not be valid: " + ex.Message);
        }

        try
        {
            var saleAnimal = SaleAnimal.Create(
                name: "Sebastian",
                animalType: AnimalType.CAT,
                species: "Sphynx",
                price: 567.89m,
                attributes: new Dictionary<string, string>
                {
                    { "Color", "Black" },
                    { "Age", "2" },
                    { "Weight", "4.5" }
                }
            );

            Console.WriteLine(saleAnimal.ToString());
        }
        catch (ValidationException ex)
        {
            Console.WriteLine("Should not be valid: " + ex.Message);
        }
    }
}











