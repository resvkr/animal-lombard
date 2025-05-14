using AnimalLombard.Modals;
using AnimalLombard.Services;
using AnimalLombard.Utils;

namespace AnimalLombard.View;

public class SaleAnimalView
{
    private readonly SaleAnimalService _saleAnimalService;

    public SaleAnimalView(SaleAnimalService saleAnimalService)
    {
        _saleAnimalService = saleAnimalService;
    }

    public void ShowSaleAnimals()
    {
        var currentPage = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("List of available animals for sale: (For exit press 'q')");
            var saleAnimals = _saleAnimalService.ShowAvailableAnimals(currentPage);

            saleAnimals.ForEach(Console.WriteLine);

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

    public void ShowAllSaleAnimals()
    {
        var currentPage = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("List of available animals for sale: (For exit press 'q')");
            var saleAnimals = _saleAnimalService.ShowAllAnimals(currentPage);

            saleAnimals.ForEach(Console.WriteLine);

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

    public void ShowBuyAnimalMenu()
    {
        Console.WriteLine("Provide id of the animal you want to buy: ");
        var id = Console.ReadLine();
        if (id is null)
        {
            Console.WriteLine("Invalid input, please try again");
            return;
        }

        var animal = _saleAnimalService.BuyAnimal(id);

        if (animal is null)
        {
            Console.WriteLine("Animal not found or not available for sale");
            return;
        }

        Console.WriteLine(animal.Name + " was added to your order");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public void ShowAddAnimalMenu()
    {
        Console.WriteLine("Would you like to add a new animal? \n You need to give some information about it.");
        Console.WriteLine("Please provide the name of the animal: ");
        var name = Console.ReadLine();
        var animalType = EnumUtils.AskEnumValue<AnimalType>("Animal type: ");
        
        Console.WriteLine("Please provide the species of the animal: ");
        var species = Console.ReadLine();
        
        Console.WriteLine("You can also provide a description or leave it empty: ");
        var description = Console.ReadLine();
        
        Console.WriteLine("Please provide the price of the animal: ");
        var price = decimal.Parse(Console.ReadLine() ?? string.Empty);

        var attributes = new Dictionary<string, string>();
        Console.WriteLine("Do you want to add any attributes? (y/n)");
        var addAttributes = Console.ReadLine();
        if (addAttributes is null || !addAttributes.Equals("y", StringComparison.CurrentCultureIgnoreCase)) return;
        Console.WriteLine("Please provide the attributes in the format 'name of attributes, value of attributes' (type 'done' to finish): ");
        while (true)
        {
            var input = Console.ReadLine();
            if (input is null || input.Equals("done", StringComparison.CurrentCultureIgnoreCase))
                break;

            var parts = input.Split(",");
            if (parts.Length != 2)
            {
                Console.WriteLine("Invalid input, please try again");
                continue;
            }
                
            var attributeName = parts[0].Trim();
            var attributeValue = parts[1].Trim();
            if (attributes.TryAdd(attributeName, attributeValue)) continue;
            Console.WriteLine("Attribute already exists, please try again");
        }
        
        var success = _saleAnimalService.AddAnimal(name, animalType, species, description, price, attributes);

        Console.WriteLine(success ? "Animal added successfully" : "Animal not added, please try again");
    }

    public void ShowDeleteAnimalMenu()
    {
        try
        {
            Console.WriteLine("Provide id of the animal you want to delete: ");
            var id = Console.ReadLine();
            _saleAnimalService.DeleteAnimal(id);
            Console.WriteLine("Animal deleted successfully");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}