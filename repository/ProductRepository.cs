using animal_lombart.interfaces;
using Models;

namespace animal_lombart.repository;

public class ProductRepository : IRepository<Product>
{
    private List<Product> products = new();
    public void Save(Product entity)
    {
        var id = products.FindIndex(u => u.Id == entity.Id);
        if (id >= 0)
        {
            products[id] = entity;
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