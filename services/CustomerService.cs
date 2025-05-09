using Models;

namespace Services;

public class CustomerService
{
    private readonly DataStore _dataStore;

    public CustomerService(DataStore dataStore)
    {
        _dataStore = dataStore;
    }
    public void ShowSaleAnimal()
    {
        int currentPage = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("List of available animals for sale: (For exit press 'q')\n");
            List<SaleAnimal> animals = _dataStore.SaleAnimalRepository.FindAllAvailable();
            for(int i = currentPage*10; i < currentPage*10 + 10 && i < animals.Count; i++)
            {
                Console.WriteLine(i + ". " + animals[i].ToString());
            }
            Console.WriteLine("\nPress 'n' for next page, 'p' for previous page, 'q' for exit");
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.N)
            {
                if (currentPage < animals.Count / 10)
                {
                    currentPage++;
                }
            }
            else if (key.Key == ConsoleKey.P)
            {
                if (currentPage > 0)
                {
                    currentPage--;
                }
            }
            else if (key.Key == ConsoleKey.Q)
            {
                break;
            }
        }
    }
    
    public SaleAnimal BuyAnimal()
    {
        Console.WriteLine("Provide id of the animal you want to buy: ");
        var id = Console.ReadLine();
        var animal = _dataStore.SaleAnimalRepository.FindById(id);
        return animal;
    }
    
    public void ShowProduct()
    {
        int CurrentPage = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("List of available products for sale: (For exit press 'q')\n");
            List<Product> products = _dataStore.ProductRepository.FindAll();
            for(int i = CurrentPage*10; i < CurrentPage*10 + 10 && i < products.Count; i++)
            {
                Console.WriteLine(i + ". " + products[i].ToString());
            }
            Console.WriteLine("\nPress 'n' for next page, 'p' for previous page, 'q' for exit");
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.N)
            {
                if (CurrentPage < products.Count / 10)
                {
                    CurrentPage++;
                }
            }
            else if (key.Key == ConsoleKey.P)
            {
                if (CurrentPage > 0)
                {
                    CurrentPage--;
                }
            }
            else if (key.Key == ConsoleKey.Q)
            {
                break;
            }
        }
    }
    
    public Product BuyProduct()
    {
        Console.WriteLine("Provide id of the product you want to buy: ");
        var id = Console.ReadLine();
        var product = _dataStore.ProductRepository.FindById(id);
        return product;
    }

    public Order PlaceOrder()
    {
        int totalPrice = 0;
        User user = AppContext.CurrentUser;
        Console.WriteLine("Enter ID of pet you would like to buy: ");
        var id = Console.ReadLine();
        var pet = _dataStore.SaleAnimalRepository.FindById(id);
        if (pet == null)
        {
            Console.WriteLine("Pet not found");
            throw new Exception("Pet not found");
        }
        totalPrice += pet.Price;
        Console.WriteLine("Do you want to add additional products? (y/n)");
        var key = Console.ReadKey();
        List<Product> products = new List<Product>();
        if (key.Key == ConsoleKey.Y)
        {
            Console.WriteLine("Enter ID of products you want to buy: (Ex: 1,2,3)");
            var ids = Console.ReadLine();
            var productIds = ids.Split(',');
            foreach (var selected_id in productIds)
            {
                products.Add(_dataStore.ProductRepository.FindById(selected_id));
                totalPrice += _dataStore.ProductRepository.FindById(selected_id).Price;
            }
        }
        else
        {
            products = null;
        }
        Console.WriteLine("Choose payment method: (Cash/Card)");
        var paymentType = Console.ReadLine();
        if (paymentType != "Cash" && paymentType != "Card")
        {
            Console.WriteLine("Invalid payment method");
            throw new Exception("Invalid payment method");
        }
        PaymentType payment = paymentType == "Cash" ? PaymentType.CASH : PaymentType.CREDIT_CARD;
        return Order.Create(user, pet, totalPrice, payment, products);
    }

    public BoardingForm PlaceBoardingForm()
    {
        Console.WriteLine("Provide information about your pet: ");
        Console.WriteLine("Name: ");
        var name = Console.ReadLine();
        Console.WriteLine("Animal type: ");
        var animalTypeString = Console.ReadLine();
        AnimalType animalType = animalTypeString switch
        {
            "Dog" => AnimalType.DOG,
            "Cat" => AnimalType.CAT,
            "Cow" => AnimalType.COW,
            "Dolphin" => AnimalType.DOLPHIN,
            "Other" => AnimalType.OTHER,
            _ => throw new Exception("Invalid animal type")
        };
        Console.WriteLine("Species: ");
        var species = Console.ReadLine();
        var owner = AppContext.CurrentUser;
        Console.WriteLine("Feeding type: ");
        var feedingTypeString = Console.ReadLine();
        FeedingType feedingType = feedingTypeString switch
        {
            "Dry" => FeedingType.DRY,
            "Wet" => FeedingType.WET,
            "Mixed" => FeedingType.MIXED,
            _ => throw new Exception("Invalid feeding type")
        };
        Console.WriteLine("Address: ");
        var address = Console.ReadLine();
        Console.WriteLine("City: ");
        var city = Console.ReadLine();
        var boardedAnimal = BoardedAnimal.Create(name, animalType, species, owner, feedingType, address, city);
        Console.WriteLine("Provide information about boarding: ");
        Console.WriteLine("Start date: ");
        var startDateString = Console.ReadLine();
        DateTime startDate = DateTime.Parse(startDateString);
        Console.WriteLine("End date: ");
        var endDateString = Console.ReadLine();
        DateTime endDate = DateTime.Parse(endDateString);
        Console.WriteLine("Payment type: ");
        var paymentTypeString = Console.ReadLine();
        PaymentType paymentType = paymentTypeString switch
        {
            "Cash" => PaymentType.CASH,
            "Card" => PaymentType.CREDIT_CARD,
            _ => throw new Exception("Invalid payment type")
        };
        return BoardingForm.Create(boardedAnimal, startDate, endDate, paymentType);
    }
}