using AnimalLombard.Modals;
using AnimalLombard.Repository.Interfaces;

namespace AnimalLombard.Repository;

public class SaleAnimalRepository : ISaleAnimalRepository
{
    private readonly List<SaleAnimal> _saleAnimals = new();
    
    public void Save(SaleAnimal entity)
    {
        var existing = _saleAnimals.Find(u => u.Id == entity.Id);
        if (existing != null)
        {
            var index = _saleAnimals.IndexOf(existing);
            _saleAnimals[index] = entity;
        }
        else
        {
            _saleAnimals.Add(entity);
        }
    }

    public SaleAnimal? FindById(string id)
    {
        return string.IsNullOrEmpty(id) 
            ? null 
            : _saleAnimals.FirstOrDefault(u => u.Id == id);
    }

    public List<SaleAnimal> FindAll()
    {
        return _saleAnimals;
    }

    public void Delete(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        _saleAnimals.RemoveAll(u => u.Id == id);
    }

    public List<SaleAnimal> FindAllAvailable()
    {
        return _saleAnimals.Where(u => u.IsAvailable).ToList();
    }
}