using Repository;
using Models;

namespace Services;

public class BoardedAnimalService : IAnimalService
{
    private readonly BoardedAnimalRepository _boardedAnimalRepository;
    public BoardedAnimalService(DataStore dataStore)
    {
        _boardedAnimalRepository = dataStore.BoardedAnimalRepository;
    }

    public void AddAnimal(Animal animal)
    {
        _boardedAnimalRepository.save(animal);
    }

    public void DeleteAnimal(string id)
    {
        _boardedAnimalRepository.delete(id);
    }

    public void UpdateAnimal(Animal animal)
    {
        _boardedAnimalRepository.save(animal);
    }
}