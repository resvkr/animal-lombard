using AnimalLombard.Modals;
using AnimalLombard.Services;

namespace AnimalLombard.View;

public class BoardedAnimalView
{
    private readonly BoardedAnimalService _boardedAnimalService;
    private readonly BoardingFormService _boardingFormService;

    public BoardedAnimalView(BoardedAnimalService boardedAnimalService, BoardingFormService boardingFormService)
    {
        _boardedAnimalService = boardedAnimalService;
        _boardingFormService = boardingFormService;
    }

    public void PlaceBoardingForm()
    {
        Console.WriteLine("Provide information about the animal you want to board: ");
        
        Console.WriteLine("Name: ");
        var name = Console.ReadLine();
        var animalType = AskEnumValue<AnimalType>("Animal type: ");
        
        Console.WriteLine("Species: ");
        var species = Console.ReadLine();
        var feedingType = AskEnumValue<FeedingType>("Feeding type: ");
        
        Console.WriteLine("Address: ");
        var address = Console.ReadLine();
        Console.WriteLine("City: ");
        var city = Console.ReadLine();

        try
        {
            var boardedAnimal = _boardedAnimalService.PlaceForm(
                name,
                animalType,
                species,
                feedingType,
                address,
                city
            );
        
            Console.WriteLine("Provide information about boarding: ");
            Console.WriteLine("Start date (yyyy-mm-dd): ");
            var startDateString = Console.ReadLine();
            
            Console.WriteLine("End date (yyyy-mm-dd): ");
            var endDateString = Console.ReadLine();
            
            var paymentType = AskEnumValue<PaymentType>("Payment type: ");
            
            var boardingForm = _boardingFormService.PlaceForm(
                boardedAnimal,
                startDateString,
                endDateString,
                paymentType
            );
            
            Console.WriteLine("Boarding form created successfully!");
        } 
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static TEnum AskEnumValue<TEnum>(string prompt) where TEnum : struct, Enum
    {
        Console.WriteLine(prompt);
        var enumValues = Enum.GetValues<TEnum>().Cast<TEnum>().ToList();

        for (var i = 0; i < enumValues.Count; i++)
        {
            Console.WriteLine($"{i}: {enumValues[i]}");
        }
        
        Console.WriteLine("Enter the name or index of the value: ");
        var input = Console.ReadLine();
        
        if (Enum.TryParse<TEnum>(input, true, out var parsedByName))
            return parsedByName;
        
        if (int.TryParse(input, out var index) && index >= 0 && index < enumValues.Count)
            return enumValues[index];
        
        throw new ArgumentException($"Invalid input: {input}. Please enter a valid name or index.");
    }
}