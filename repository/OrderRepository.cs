
using AnimalLombart.Repository.Interfaces;
using Models;

namespace AnimaLombart.Repository;

public class OrderRepository : IRepository<Order>
{
    private List<Order> orders = new();
    public void Save(Order entity)
    {
        var existing = orders.Find(u => u.Id == entity.Id);
        if (existing != null)
        {
            var index = orders.IndexOf(existing);
            orders[index] = entity;
        }
        else
        {
            orders.Add(entity);
        }
    }

    public Order FindById(string id)
    {
        if (string.IsNullOrEmpty(id)) return null;
        return orders.FirstOrDefault(u => u.Id == id);
    }

    public List<Order> FindAll()
    {
        return orders;
    }

    public void Delete(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        orders.RemoveAll(u => u.Id == id);
    }
}