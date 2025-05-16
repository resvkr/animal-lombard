using AnimalLombard.Repository.Interfaces;
using AnimalLombard.Modals;

namespace AnimalLombard.Repository;

public class BoardingFormRepository : IBoardingFormRepository
{
    private List<BoardingForm> boardingForms = new();
    public void Save(BoardingForm entity)
    {
        var existing = boardingForms.Find(u => u.Id == entity.Id);
        if (existing != null)
        {
            var index = boardingForms.IndexOf(existing);
            boardingForms[index] = entity;
        }
        else
        {
            boardingForms.Add(entity);
        }
    }

    public BoardingForm? FindById(string id)
    {
        return string.IsNullOrEmpty(id) 
            ? null 
            : boardingForms.FirstOrDefault(u => u.Id == id);
    }

    public List<BoardingForm> FindAll()
    {
        return boardingForms;
    }

    public void Delete(string id)
    {
        if (string.IsNullOrEmpty(id)) return;
        boardingForms.RemoveAll(u => u.Id == id);
    }
}