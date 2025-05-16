using AnimalLombard.Modals;

namespace AnimalLombard.Services.Interfaces;

public interface IAnimalService<in T> where T : Animal
{
    public void AddAnimal(T animal);
    public void DeleteAnimal(string id);
    public void UpdateAnimal(T animal);
}