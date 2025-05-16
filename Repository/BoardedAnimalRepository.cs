
using AnimalLombard.Repository.Interfaces;
using AnimalLombard.Modals;

namespace AnimalLombard.Repository;

public class BoardedAnimalRepository : IBoardedAnimalRepository
{
    private List<BoardedAnimal> boardedAnimals = new();
    public void Save(BoardedAnimal entity)
    {
        var existing = boardedAnimals.Find(u => u.Id == entity.Id);
        if (existing != null)
        {
            var index = boardedAnimals.IndexOf(existing);
            boardedAnimals[index] = entity;
        }
        else
        {
            boardedAnimals.Add(entity);
        }
    }

    public BoardedAnimal? FindById(string id)
    {
        return string.IsNullOrEmpty(id) 
            ? null 
            : boardedAnimals.FirstOrDefault(u => u.Id == id);
    }

    public List<BoardedAnimal> FindByUserId(string userId)
    {
        return boardedAnimals.Where(u => string.Equals(u.OwnerId, userId)).ToList();
    }

    public List<BoardedAnimal> FindAll()
    {
        return boardedAnimals;
    }

    public void Delete(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        boardedAnimals.RemoveAll(u => u.Id == id);
    }
}
