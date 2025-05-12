
using AnimalLombart.Repository;
using AnimalLombart.Repository.Interfaces;
using Models;

namespace AnimaLombart.Repository;

public class DataStore
{
    public UserRepository UserRepository { get; }
    public SaleAnimalRepository SaleAnimalRepository { get; }
    public BoardedAnimalRepository BoardedAnimalRepository { get; }
    public ProductRepository ProductRepository { get; }
    public OrderRepository OrderRepository { get; }
    public BoardingFormRepository BoardingFormRepository { get; }

    public DataStore(
        UserRepository userRepo,
        SaleAnimalRepository saleAnimalRepo,
        BoardedAnimalRepository boardedAnimalRepo,
        ProductRepository productRepo,
        OrderRepository orderRepo,
        BoardingFormRepository boardingFormRepo)
    {
        UserRepository = userRepo;
        SaleAnimalRepository = saleAnimalRepo;
        BoardedAnimalRepository = boardedAnimalRepo;
        ProductRepository = productRepo;
        OrderRepository = orderRepo;
        BoardingFormRepository = boardingFormRepo;
    }
}