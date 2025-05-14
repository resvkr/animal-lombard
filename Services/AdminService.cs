using AnimalLombard.Repository;
using AnimalLombard.Modals;

namespace AnimalLombard.Services;

public class AdminService
{
    private readonly DataStore _dataStore;

    public AdminService(DataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public List<SaleAnimal> GetSaleAnimalsList()
    {
        return _dataStore.SaleAnimalRepository.FindAllAvailable();
    }

    public List<BoardedAnimal> GetBoardedAnimalsList()
    {
        return _dataStore.BoardedAnimalRepository.FindAll();
    }

    public List<Product> GetProductsList()
    {
        return _dataStore.ProductRepository.FindAll();
    }

    public void BanClient(string? userId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentNullException(userId, "User ID cannot be null or empty.");

        var user = _dataStore.UserRepository.FindById(userId);
        if (user is null) 
            throw new ArgumentNullException(userId, "User not found.");

        user.IsActiveProfile = false;
        _dataStore.UserRepository.Save(user);
    }

    public List<User> GetUsers(int page = 0, int size = 10)
    {
        var users = _dataStore.UserRepository.FindAll();

        return users.Skip(page * size).Take(size).ToList();
    }
}