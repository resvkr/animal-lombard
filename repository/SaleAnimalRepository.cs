
using AnimalLombart.Repository.Interfaces;
using Models;

namespace AnimaLombart.Repository;

public class SaleAnimalRepository : IRepository<SaleAnimal>
{
    private List<SaleAnimal> saleAnimals = new();
    
    
    
    public void Save(SaleAnimal entity)
    {
        var existing = saleAnimals.Find(u => u.Id == entity.Id);
        if (existing != null)
        {
            var index = saleAnimals.IndexOf(existing);
            saleAnimals[index] = entity;
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