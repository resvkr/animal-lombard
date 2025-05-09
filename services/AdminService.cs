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
        return _dataStore.SaleAnimalsRepository.FindAllAvailable();
    }
    public List<BoardedAnimal> GetBoardedAnimalsList()
    {
        return _dataStore.BoardedAnimalsRepository.FindAll();
    }
    public List<Product> GetProductsList()
    {
        return _dataStore.ProductsRepository.FindAll();
    }

    public void banClient(string userId)
    {
        _dataStore.UsersRepository.findById(userId).IsActiveProfile = false;
    }
}