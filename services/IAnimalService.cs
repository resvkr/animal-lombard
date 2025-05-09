using Models;

namespace Services;

public interface IAnimalService
{
    public void AddAnimal(Animal animal);
    public void DeleteAnimal(string id);
    public void UpdateAnimal(Animal animal);
}