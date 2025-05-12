using AnimalLombart.Repository;
using AnimaLombart.Repository;
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
        _boardedAnimalRepository.Save((BoardedAnimal)animal);
    }

    public void DeleteAnimal(string id)
    {
        _boardedAnimalRepository.Delete(id);
    }

    public void UpdateAnimal(Animal animal)
    {
        _boardedAnimalRepository.Save((BoardedAnimal)animal);
    }
}