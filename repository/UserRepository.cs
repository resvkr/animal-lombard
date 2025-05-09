using animal_lombart.interfaces;
using Models;

namespace animal_lombart.repository;

public class UserRepository : IRepository<User>
{
    private readonly List<User> users = new List<User>();
    
    
    public void Save(User entity)
    {
        var id = users.FindIndex(u => u.Id == entity.Id);
        if (id >= 0)
        {
            users[id] = entity;
        }
        else
        {
            users.Add(entity);
        }
    }

    public User FindById(string id)
    {
        if (string.IsNullOrEmpty(id)) return null;
        return users.FirstOrDefault(u => u.Id == id);
    }

    public List<User> FindAll()
    {
        return users;
    }

    public void Delete(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        users.RemoveAll(u => u.Id == id);
    }

    public User FindByEmail(string email)
    {
        return users.FirstOrDefault(u => u.Email == email);
    }

    public User FindByPhone(string phone)
    {
        return users.FirstOrDefault(u => u.PhoneNumber == phone);
    }
}