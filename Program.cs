using Models;

class Program
{
    public static void Main()
    {
        User user = User.Create("John Doe");
        Console.WriteLine($"User created with ID: {user.Id} and Name: {user.Name}");
    }
}











