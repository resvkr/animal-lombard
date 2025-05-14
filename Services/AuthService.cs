using System.ComponentModel.DataAnnotations;
using AnimalLombard.Context.Interfaces;
using AnimalLombard.Modals;
using AnimalLombard.Repository.Interfaces;
using AnimalLombard.Utils;
using AnimalLombard.Repository;

namespace AnimalLombard.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IMutableUserContext _userContext;

    public AuthService(DataStore dataStore, IMutableUserContext userContext)
    {
        _userRepository = dataStore.UserRepository;
        _userContext = userContext;
    }

    public User Login(string email, string password)
    {
        var user = _userRepository.FindByEmail(email);
        if (user is null) throw new ArgumentException("User not found");
        
        if (user.IsActiveProfile == false) throw new ArgumentException("User has been banned");

        if (!HashUtils.VerifyPassword(password, user.PasswordHash)) 
            throw new ArgumentException("Invalid password");
        
        _userContext.SetCurrentUser(user);
        return user;
    }

    public void Register(string name, string phone, string email, string password)
    {
        var user = _userRepository.FindByEmail(email);
        if (user != null) throw new ArgumentException("User already exists");

        var hashedPassword = HashUtils.HashPassword(password);
        try
        {
            user = User.Create(name, phone, email, hashedPassword);
            _userRepository.Save(user);
        }
        catch (ValidationException e)
        {
            throw new ArgumentException(e.Message);
        }
    }

    public void Logout()
    {
        _userContext.SetCurrentUser(null);
    }
}