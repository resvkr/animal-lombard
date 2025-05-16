using AnimalLombard.Context.Interfaces;
using AnimalLombard.Repository;
using AnimalLombard.Modals;
using AnimalLombard.Repository.Interfaces;
using AnimalLombard.Services.Interfaces;

namespace AnimalLombard.Services;

public class BoardedAnimalService : IAnimalService<BoardedAnimal>
{
    private readonly IBoardedAnimalRepository _boardedAnimalRepository;
    private readonly IReadOnlyUserContext _userContext;
    public BoardedAnimalService(DataStore dataStore, IReadOnlyUserContext userContext)
    {
        _boardedAnimalRepository = dataStore.BoardedAnimalRepository;
        _userContext = userContext;
    }
    
    public BoardedAnimal? PlaceForm(
        string? name,
        AnimalType animalType,
        string? species,
        FeedingType feedingType,
        string? address,
        string? city
    )
    {
        if (_userContext.CurrentUser is null)
            throw new InvalidOperationException("User is not logged in.");
        
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(species) ||
            string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("All fields must be filled out.");

        var boardedAnimal = BoardedAnimal.Create(
            name,
            animalType,
            species,
            _userContext.CurrentUser.Id,
            feedingType,
            address,
            city
        );
        
        _boardedAnimalRepository.Save(boardedAnimal);
        return boardedAnimal;
    }

    public List<BoardedAnimal> GetBoardedAnimalsList(int page = 0, int pageSize = 10)
    {
        var userId = _userContext.CurrentUser?.Id;
        
        if (userId is null)
            throw new InvalidOperationException("User is not logged in.");
        
        if (page < 0) throw new ArgumentOutOfRangeException(nameof(page), "Page must be >= 0");
        if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "PageSize must be > 0");

        var boardedAnimals = _boardedAnimalRepository.FindByUserId(userId);
        
        return boardedAnimals
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
    }
    public void AddAnimal(BoardedAnimal animal)
    {
        _boardedAnimalRepository.Save(animal);
    }

    public void DeleteAnimal(string id)
    {
        _boardedAnimalRepository.Delete(id);
    }

    public void UpdateAnimal(BoardedAnimal animal)
    {
        _boardedAnimalRepository.Save(animal);
    }
}