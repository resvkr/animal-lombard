using animal_lombart.interfaces;
using Models;

namespace animal_lombart.repository;

public class SaleAnimalRepository : IRepository<SaleAnimal>
{
    private List<SaleAnimal> saleAnimals = new();
    
    
    
    public void Save(SaleAnimal entity)
    {
        var id = saleAnimals.FindIndex(u => u.Id == entity.Id);
        if (id >= 0)
        {
            saleAnimals[id] = entity;
        }
        else
        {
            saleAnimals.Add(entity);
        }
    }

    public SaleAnimal FindById(string id)
    {
        if (string.IsNullOrEmpty(id)) return null;
        return saleAnimals.FirstOrDefault(u => u.Id == id);
    }

    public List<SaleAnimal> FindAll()
    {
        return saleAnimals;
    }

    public void Delete(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        saleAnimals.RemoveAll(u => u.Id == id);
    }

    public List<SaleAnimal> FindAllAvailable()
    {
        return saleAnimals.Where(u => u.isAvailable).ToList();
    }
}