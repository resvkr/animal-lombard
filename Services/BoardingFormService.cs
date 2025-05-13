using AnimalLombard.Modals;
using AnimalLombard.Repository;
using AnimalLombard.Repository.Interfaces;

namespace AnimalLombard.Services;

public class BoardingFormService
{
    private readonly IBoardingFormRepository _boardingFormRepository;
    
    public BoardingFormService(DataStore dataStore)
    {
        _boardingFormRepository = dataStore.BoardingFormRepository;
    }

    public BoardingForm PlaceForm(
        BoardedAnimal? boardedAnimal,
        string? startDateString,
        string? endDateString,
        PaymentType paymentType
    )
    {
        if (boardedAnimal is null)
            throw new InvalidOperationException("Boarded animal is null.");
        
        if (string.IsNullOrWhiteSpace(startDateString) || string.IsNullOrWhiteSpace(endDateString))
            throw new ArgumentException("All fields must be filled out.");
        
        if (!DateTime.TryParse(startDateString, out var startDate))
            throw new ArgumentException("Invalid start date format.");
        if (!DateTime.TryParse(endDateString, out var endDate))
            throw new ArgumentException("Invalid end date format.");
        
        var timeDifference = endDate - startDate;
        if (timeDifference.Days < 0)
            throw new ArgumentException("End date must be after start date.");
        
        var boardingPrice = timeDifference.Days * 20 + 50 * Convert.ToDecimal(boardedAnimal.FeedingType);
        
        var boardingForm = BoardingForm.Create(
            boardedAnimal,
            startDate,
            endDate,
            paymentType,
            boardingPrice
        );
        
        _boardingFormRepository.Save(boardingForm);
        return boardingForm;
    }
}