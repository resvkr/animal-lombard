using Repository;
using Models;

namespace Services;

public class SaleAnimalService : IAnimalService
{
    private readonly SaleAnimalRepository _saleAnimalRepository;
    public SaleAnimalService(DataStore dataStore)
    {
        _saleAnimalRepository = dataStore.SaleAnimalRepository;
    }
    public void AddAnimal(Animal animal)
    {
        _saleAnimalRepository.save(animal);
    }

    public void DeleteAnimal(string id)
    {
        _saleAnimalRepository.delete(id);
    }

    public void UpdateAnimal(Animal animal)
    {
        _saleAnimalRepository.save(animal);
    }
}