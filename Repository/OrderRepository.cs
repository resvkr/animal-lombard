using AnimalLombard.Modals;
using AnimalLombard.Repository.Interfaces;

namespace AnimalLombard.Repository;

public class OrderRepository : IOrderRepository
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

    public Order? FindById(string id)
    {
        return string.IsNullOrEmpty(id) 
            ? null 
            : orders.FirstOrDefault(u => u.Id == id);
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