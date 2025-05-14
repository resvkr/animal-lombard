using System.ComponentModel.DataAnnotations;
using AnimalLombard.Modals;
using AnimalLombard.Repository.Interfaces;
using AnimalLombard.Services.Interfaces;
using AnimalLombard.Repository;
using AnimalLombard.Context.Interfaces;

namespace AnimalLombard.Services;

public class SaleAnimalService : IAnimalService<SaleAnimal>
{
    private readonly ISaleAnimalRepository _saleAnimalRepository;
    private readonly IOrderContext _orderContext;
    
    public SaleAnimalService(DataStore dataStore, IOrderContext orderContext)
    {
        _saleAnimalRepository = dataStore.SaleAnimalRepository;
        _orderContext = orderContext;
    }
    
    public List<SaleAnimal> ShowAvailableAnimals(int page = 0, int pageSize = 10)
    {
        if (page < 0) throw new ArgumentOutOfRangeException(nameof(page), "Page must be >= 0");
        if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "PageSize must be > 0");
        
        var saleAnimals = _saleAnimalRepository.FindAllAvailable();
        return saleAnimals
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
    }
    public List<SaleAnimal> ShowAllAnimals(int page = 0, int pageSize = 10)
    {
        if (page < 0) throw new ArgumentOutOfRangeException(nameof(page), "Page must be >= 0");
        if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "PageSize must be > 0");
        
        var saleAnimals = _saleAnimalRepository.FindAll();
        return saleAnimals
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
    }
    public SaleAnimal? BuyAnimal(string id)
    {
        var animal = _saleAnimalRepository.FindById(id);

        if (animal is null) return null;

        var order = _orderContext.CurrentOrder;
        try
        {
            if (order is null) _orderContext.StartNewOrder();
            _orderContext.AddSaleAnimalToOrder(animal);
            animal.IsAvailable = false;
            _saleAnimalRepository.Save(animal);
            return animal;
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    public void AddAnimal(SaleAnimal animal)
    {
        _saleAnimalRepository.Save(animal);
    }

    public bool AddAnimal(string? name, AnimalType animalType, string? species, string? description, decimal price,
        Dictionary<string, string>? attributes)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(species) || price <= 0) return false;

        try
        {
            var saleAnimal = SaleAnimal.Create(name, animalType, species, description ?? "", price,
                attributes: attributes);
            _saleAnimalRepository.Save(saleAnimal);
            return true;
        }
        catch (ValidationException ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }

    public void DeleteAnimal(string? id)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentNullException(nameof(id));
        
        _saleAnimalRepository.Delete(id);
    }

    public void UpdateAnimal(SaleAnimal animal)
    {
        _saleAnimalRepository.Save(animal);
    }
}