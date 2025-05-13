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

    public void BanClient(string userId)
    {
        _dataStore.UserRepository.FindById(userId).IsActiveProfile = false;
    }
}