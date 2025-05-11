
using AnimalLombart.Repository.Interfaces;
using Models;

namespace AnimaLombart.Repository;

public class ProductRepository : IRepository<Product>
{
    private List<Product> products = new();
    public void Save(Product entity)
    {
        var existing = products.Find(u => u.Id == entity.Id);
        if (existing != null)
        {
            var index = products.IndexOf(existing);
            products[index] = entity;
        }
        else
        {
            products.Add(entity);
        }
    }

    public Product FindById(string id)
    {
        if (string.IsNullOrEmpty(id)) return null;
        return products.FirstOrDefault(u => u.Id == id);
    }

    public List<Product> FindAll()
    {
        return products;
    }

    public void Delete(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        products.RemoveAll(u => u.Id == id);
    }
}