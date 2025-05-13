using AnimalLombard.Context.Interfaces;
using AnimalLombard.Modals;

namespace AnimalLombard.View;

public class MainView
{
    private readonly IReadOnlyUserContext _userContext;
    private readonly SaleAnimalView _saView;
    private readonly AuthView _authView;
    private readonly ProductView _productView;
    private readonly OrderView _orderView;
    private readonly BoardedAnimalView _boardedAnimalView;

    public MainView(
        IReadOnlyUserContext userContext, 
        SaleAnimalView saView, 
        AuthView authView, 
        ProductView productView,
        OrderView orderView,
        BoardedAnimalView boardedAnimalView
        )
    {
        _userContext = userContext;
        _saView = saView;
        _authView = authView;
        _productView = productView;
        _orderView = orderView;
        _boardedAnimalView = boardedAnimalView;
    }

    public void ShowMainMenu()
    {
        var isRunning = true;

        while (isRunning)
        {
            switch (_userContext.CurrentUser)
            {
                case { Role: Role.USER_ROLE }:
                    AuthorizedUserMenu();
                    break;
                case { Role: Role.ADMIN_ROLE }:
                    AuthorizedAdminMenu();
                    break;
                default:
                    NonAuthorizedUserMenu();
                    break;
            }

            Console.WriteLine("Do you want to exit? (y/N)");
            if (Console.ReadLine()?.Trim().Equals("y", StringComparison.OrdinalIgnoreCase) == true)
                isRunning = false;
        }
    }

    private void NonAuthorizedUserMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Welcome to Animal Lombard!");
        Console.WriteLine("It is a place where you can find your new friend or give your animal for a time to take a rest.");
        Console.WriteLine();
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Show available animals");
        Console.WriteLine("4. Add animal to order");
        Console.WriteLine("5. Show available products");
        Console.WriteLine("6. Add product to order");
        Console.WriteLine("7. Show your order");
        Console.WriteLine("8. Place order");
        Console.WriteLine("9. Clear order");
        Console.WriteLine("10. Exit");

        Console.WriteLine("Enter a number: ");
        if (!int.TryParse(Console.ReadLine(), out var number)) return;
        switch (number)
        {
            case 1: _authView.ShowLogin(); break;
            case 2: _authView.ShowRegister(); break;
            case 3: _saView.ShowSaleAnimals(); break;
            case 4: _saView.ShowBuyAnimalMenu(); break;
            case 5: _productView.ShowProducts(); break;
            case 6: _productView.ShowBuyProductMenu(); break;
            case 7: _orderView.ShowOrder(); break;
            case 8: _orderView.PlaceOrder(); break;
            case 9: _orderView.ClearOrder(); break;
            case 10: return;
        }
    }

    private void AuthorizedUserMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Welcome to Animal Lombard!");
        Console.WriteLine("It is a place where you can find your new friend or give your animal for a time to take a rest.");
        Console.WriteLine();
        Console.WriteLine("1. Show available animals");
        Console.WriteLine("2. Add animal to order");
        Console.WriteLine("3. Show available products");
        Console.WriteLine("4. Add product to order");
        Console.WriteLine("5. Show your order");
        Console.WriteLine("6. Place order");
        Console.WriteLine("7. Clear order");
        Console.WriteLine("8. Left your animal for boarding");
        Console.WriteLine("9. Logout");

        Console.WriteLine("Enter a number: ");
        if (!int.TryParse(Console.ReadLine(), out var number)) return;
        switch (number)
        {
            case 1: _saView.ShowSaleAnimals(); break;
            case 2: _saView.ShowBuyAnimalMenu(); break;
            case 3: _productView.ShowProducts(); break;
            case 4: _productView.ShowBuyProductMenu(); break;
            case 5: _orderView.ShowOrder(); break;
            case 6: _orderView.PlaceOrder(); break;
            case 7: _orderView.ClearOrder(); break;
            case 8: _boardedAnimalView.PlaceBoardingForm(); break;
            case 9: _authView.Logout(); break;
        }
    }

    private void AuthorizedAdminMenu()
    {
    }
}