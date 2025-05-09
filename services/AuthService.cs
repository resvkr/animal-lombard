using Models;

namespace Services;

public class AuthService
{
    private readonly UserRepository _userRepository;

    public AuthService(DataStore dataStore)
    {
        _userRepository = dataStore.UserRepository;
    }

    public User Login(string email, string passwordHash)
    {
        User user = _userRepository.FindByEmail(email);
        if (user == null) throw new Exception("User not found");
        if (user.PasswordHash == passwordHash)
        {
            return user;
        }
        throw new Exception("Invalid password");
    }

    public void Register(string name, string phone, string email, string passwordHash)
    {
        User user = _userRepository.FindByEmail(email);
        if (user != null) throw new Exception("User already exists");
        user = User.Create(name, phone, email, passwordHash);
        _userRepository.save(user);
    }

    public void logout()
    {
        AppContext.getInstance().CurrentUser = null;
    }
}