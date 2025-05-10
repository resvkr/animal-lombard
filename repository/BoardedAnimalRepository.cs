using animal_lombart.interfaces;
using Models;

namespace animal_lombart.repository;

public class BoardedAnimalRepository : IRepository<BoardedAnimal>
{
    private List<BoardedAnimal> boardedAnimals = new();
    public void Save(BoardedAnimal entity)
    {
        var id = boardedAnimals.FindIndex(u => u.Id == entity.Id);
        if (id >= 0)
        {
            boardedAnimals[id] = entity;
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
