using AnimalLombard.Services;

namespace AnimalLombard.View;

public class AuthView
{
    private readonly AuthService _authService;

    public AuthView(AuthService authService)
    {
        _authService = authService;
    }

    public void ShowLogin()
    {
        Console.WriteLine("Login");
        Console.WriteLine("Enter your email: ");
        var email = Console.ReadLine();
        Console.WriteLine("Enter your password: ");
        var password = Console.ReadLine();
        if (string.IsNullOrEmpty(email?.Trim()) || string.IsNullOrEmpty(password?.Trim())) 
        {
            Console.WriteLine("Invalid input, please try again");
            return;
        }

        try
        {
            var user = _authService.Login(email, password);
            Console.WriteLine($"Welcome {user.Name}!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void ShowRegister()
    {
        Console.WriteLine("Register");
        Console.WriteLine("Enter your name: ");
        var name = Console.ReadLine();
        Console.WriteLine("Enter your phone: ");
        var phone = Console.ReadLine();
        Console.WriteLine("Enter your email: ");
        var email = Console.ReadLine();
        Console.WriteLine("Enter your password: ");
        var password = Console.ReadLine();
        
        if (string.IsNullOrEmpty(name?.Trim()) || string.IsNullOrEmpty(phone?.Trim()) || 
            string.IsNullOrEmpty(email?.Trim()) || string.IsNullOrEmpty(password?.Trim()))
        {
            Console.WriteLine("Invalid input, please try again");
            return;
        }

        try
        {
            _authService.Register(name, phone, email, password);
            Console.WriteLine("Registration successful!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        } 
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void Logout()
    {
        Console.WriteLine("Do you want to logout? (y/n)");
        var answer = Console.ReadLine();
        if (answer?.Trim().Equals("y", StringComparison.OrdinalIgnoreCase) == true)
        {
            _authService.Logout();
            Console.WriteLine("You have been logged out.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Logout cancelled.");
        }
    }
}