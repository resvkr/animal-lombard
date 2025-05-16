
using AnimalLombard.Repository.Interfaces;

namespace AnimalLombard.Repository;

public class DataStore : IDataStore
{
    public IUserRepository UserRepository { get; }
    public ISaleAnimalRepository SaleAnimalRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IBoardingFormRepository BoardingFormRepository { get; }
    public IBoardedAnimalRepository BoardedAnimalRepository { get; }
    
    public DataStore(
        IUserRepository userRepository,
        ISaleAnimalRepository saleAnimalRepository,
        IProductRepository productRepository,
        IOrderRepository orderRepository,
        IBoardingFormRepository boardingFormRepository,
        IBoardedAnimalRepository boardedAnimalRepository
    )
    {
        UserRepository = userRepository;
        SaleAnimalRepository = saleAnimalRepository;
        ProductRepository = productRepository;
        OrderRepository = orderRepository;
        BoardingFormRepository = boardingFormRepository;
        BoardedAnimalRepository = boardedAnimalRepository;
    }
}