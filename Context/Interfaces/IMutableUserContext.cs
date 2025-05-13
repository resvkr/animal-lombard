using AnimalLombard.Modals;

namespace AnimalLombard.Context.Interfaces;

public interface IMutableUserContext : IReadOnlyUserContext
{
    void SetCurrentUser(User? user);
}