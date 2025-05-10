using animal_lombart.interfaces;
using Models;

namespace animal_lombart.repository;

public class OrderRepository : IRepository<Order>
{
    private List<Order> orders = new();
    public void Save(Order entity)
    {
        var id = orders.FindIndex(u => u.Id == entity.Id);
        if (id >= 0)
        {
            orders[id] = entity;
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