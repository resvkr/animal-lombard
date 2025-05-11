
using AnimalLombart.Repository.Interfaces;
using Models;

namespace AnimaLombart.Repository;

public class UserRepository : IRepository<User>
{
    private readonly List<User> users = new List<User>();
    
    
    public void Save(User entity)
    {
        var existing = users.Find(u => u.Id == entity.Id);
        if (existing != null)
        {
            var index = users.IndexOf(existing);
            users[index] = entity;
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