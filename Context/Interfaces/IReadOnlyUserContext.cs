using AnimalLombard.Modals;

namespace AnimalLombard.Context.Interfaces;

public interface IReadOnlyUserContext
{
    User? CurrentUser { get; }
}