
using AnimalLombart.Repository.Interfaces;
using Models;

namespace AnimalLombart.Repository;

public class BoardedAnimalRepository : IRepository<BoardedAnimal>
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

    public BoardedAnimal FindById(string id)
    {
        if (string.IsNullOrEmpty(id)) return null;
        return boardedAnimals.FirstOrDefault(u => u.Id == id);
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
