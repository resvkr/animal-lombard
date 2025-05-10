
using AnimalLombart.Repository.Interfaces;
using Models;

namespace AnimaLombart.Repository;

public class DataStore
{
    public IRepository<User> UserRepository { get; }
    public IRepository<SaleAnimal> SaleAnimalRepository { get; }
    public IRepository<BoardedAnimal> BoardedAnimalRepository { get; }
    public IRepository<Product> ProductRepository { get; }
    public IRepository<Order> OrderRepository { get; }
    public IRepository<BoardingForm> BoardingFormRepository { get; }

    public DataStore(
        IRepository<User> userRepo,
        IRepository<SaleAnimal> saleAnimalRepo,
        IRepository<BoardedAnimal> boardedAnimalRepo,
        IRepository<Product> productRepo,
        IRepository<Order> orderRepo,
        IRepository<BoardingForm> boardingFormRepo)
    {
        UserRepository = userRepo;
        SaleAnimalRepository = saleAnimalRepo;
        BoardedAnimalRepository = boardedAnimalRepo;
        ProductRepository = productRepo;
        OrderRepository = orderRepo;
        BoardingFormRepository = boardingFormRepo;
    }
}