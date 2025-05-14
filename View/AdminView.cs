using AnimalLombard.Services;

namespace AnimalLombard.View;

public class AdminView
{
    private readonly AdminService _adminService;

    public AdminView(AdminService adminService)
    {
        _adminService = adminService;
    }

    public void ShowUsers()
    {
        int currentPage = 0;
        while (true)
        {
            Console.WriteLine("You have left: ");
            var users = _adminService.GetUsers(currentPage);
            
            users.ForEach(Console.WriteLine);

            Console.WriteLine("Press 'n' for next page, 'p' for previous page, 'q' for exit");
            var key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.N: currentPage++; break;
                case ConsoleKey.P: currentPage--; break;
                case ConsoleKey.Q: return;
                default: Console.WriteLine("Invalid input, please try again"); break;
            }
        }
    }

    public void ShowBanClientMenu()
    {
        try
        {
            Console.WriteLine("provide user id to ban: ");
            var userId = Console.ReadLine();
            _adminService.BanClient(userId);
            Console.WriteLine("User banned successfully");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}