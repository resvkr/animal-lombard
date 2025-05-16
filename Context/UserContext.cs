using AnimalLombard.Context.Interfaces;
using AnimalLombard.Modals;

namespace AnimalLombard.Context;

public class UserContext : IMutableUserContext
{
    public User? CurrentUser { get; private set; }

    public void SetCurrentUser(User? user)
    {
        CurrentUser = user;
    }
}