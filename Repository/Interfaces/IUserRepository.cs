using AnimalLombard.Modals;

namespace AnimalLombard.Repository.Interfaces;

public interface IUserRepository : IRepository<User>
{
    User FindByEmail(string email);
    User FindByPhone(string phoneNumber);
}