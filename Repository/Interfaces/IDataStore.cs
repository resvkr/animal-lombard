namespace AnimalLombard.Repository.Interfaces;

public interface IDataStore
{
    IUserRepository UserRepository { get; }
    ISaleAnimalRepository SaleAnimalRepository { get; }
    IProductRepository ProductRepository { get; }
    IOrderRepository OrderRepository { get; }
    IBoardingFormRepository BoardingFormRepository { get; }
    IBoardedAnimalRepository BoardedAnimalRepository { get; }
}