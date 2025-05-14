using AnimalLombard.Modals;
using AnimalLombard.Services;
using AnimalLombard.Utils;

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

    public void ShowBoardingAnimals()
    {
        var currentPage = 0;
        while (true)
        {
            Console.WriteLine("You have left: ");
            var boardedAnimals = _boardedAnimalService.GetBoardedAnimalsList(currentPage);
            
            boardedAnimals.ForEach(Console.WriteLine);

            Console.WriteLine("Press 'n' for next page, 'p' for previous page, 'q' for exit");
            var key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.N: currentPage++; break;
                case ConsoleKey.P: currentPage--; break;
                case ConsoleKey.Q: return;
                default: Console.WriteLine("Invalid input, please try again"); break;
            }
        }
    }
    public void PlaceBoardingForm()
    {
        Console.WriteLine("Provide information about the animal you want to board: ");
        
        Console.WriteLine("Name: ");
        var name = Console.ReadLine();
        var animalType = EnumUtils.AskEnumValue<AnimalType>("Animal type: ");
        
        Console.WriteLine("Species: ");
        var species = Console.ReadLine();
        var feedingType = EnumUtils.AskEnumValue<FeedingType>("Feeding type: ");
        
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
            
            var paymentType = EnumUtils.AskEnumValue<PaymentType>("Payment type: ");
            
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

    
}