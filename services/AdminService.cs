using AnimaLombart.Repository;
using Models;

namespace Services;

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

    public void banClient(string userId)
    {
        _dataStore.UserRepository.FindById(userId).IsActiveProfile = false;
    }
}