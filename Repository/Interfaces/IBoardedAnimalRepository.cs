using AnimalLombard.Modals;

namespace AnimalLombard.Repository.Interfaces;

public interface IBoardedAnimalRepository : IRepository<BoardedAnimal>
{
    public List<BoardedAnimal> FindByUserId(string userId);
}