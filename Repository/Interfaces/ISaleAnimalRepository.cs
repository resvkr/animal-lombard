using AnimalLombard.Modals;

namespace AnimalLombard.Repository.Interfaces;

public interface ISaleAnimalRepository : IRepository<SaleAnimal>
{
    List<SaleAnimal> FindAllAvailable();
}