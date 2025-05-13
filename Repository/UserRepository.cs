using AnimalLombard.Modals;
using AnimalLombard.Repository.Interfaces;

namespace AnimalLombard.Repository;
public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new List<User>();
    public void Save(User entity)
    {
        var existing = _users.Find(u => u.Id == entity.Id);
        if (existing != null)
        {
            var index = _users.IndexOf(existing);
            _users[index] = entity;
        }
        else
        {
            _users.Add(entity);
        }
    }

    public User? FindById(string id)
    {
        return string.IsNullOrEmpty(id) 
            ? null 
            : _users.FirstOrDefault(u => u.Id == id);
    }

    public List<User> FindAll()
    {
        return _users;
    }

    public void Delete(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        _users.RemoveAll(u => u.Id == id);
    }

    public User FindByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email == email);
    }

    public User FindByPhone(string phone)
    {
        return _users.FirstOrDefault(u => u.PhoneNumber == phone);
    }
}