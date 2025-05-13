using AnimalLombard.Modals;
using AnimalLombard.Services;

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
}