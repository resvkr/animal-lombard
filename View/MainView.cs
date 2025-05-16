using AnimalLombard.Context.Interfaces;
using AnimalLombard.Modals;

namespace AnimalLombard.View;

public class MainView(
    IReadOnlyUserContext userContext,
    SaleAnimalView saView,
    AuthView authView,
    ProductView productView,
    OrderView orderView,
    BoardedAnimalView boardedAnimalView,
    AdminView adminView
    )
{
    public void ShowMainMenu()
    {
        var isRunning = true;

        while (isRunning)
        {
            switch (userContext.CurrentUser)
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
        while (true)
        {
            Console.Clear();
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
                case 1: authView.ShowLogin(); return;
                case 2: authView.ShowRegister(); break;
                case 3: saView.ShowSaleAnimals(); break;
                case 4: saView.ShowBuyAnimalMenu(); break;
                case 5: productView.ShowProducts(); break;
                case 6: productView.ShowBuyProductMenu(); break;
                case 7: orderView.ShowOrder(); break;
                case 8: orderView.PlaceOrder(); break;
                case 9: orderView.ClearOrder(); break;
                case 10: return;
            }
        }
    }

    private void AuthorizedUserMenu()
    {
        while (true)
        {
            Console.Clear();
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
            Console.WriteLine("9. Show your boarding animals");
            Console.WriteLine("10. Logout");

            Console.WriteLine("Enter a number: ");
            if (!int.TryParse(Console.ReadLine(), out var number)) return;
            switch (number)
            {
                case 1: saView.ShowSaleAnimals(); break;
                case 2: saView.ShowBuyAnimalMenu(); break;
                case 3: productView.ShowProducts(); break;
                case 4: productView.ShowBuyProductMenu(); break;
                case 5: orderView.ShowOrder(); break;
                case 6: orderView.PlaceOrder(); break;
                case 7: orderView.ClearOrder(); break;
                case 8: boardedAnimalView.PlaceBoardingForm(); break;
                case 9: boardedAnimalView.ShowBoardingAnimals(); break;
                case 10: authView.Logout();
                    return;
            }
        }
    }

    private void AuthorizedAdminMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Animal Lombard!");
            Console.WriteLine("Hello, Admin");
            Console.WriteLine("1. Add animal to Database");
            Console.WriteLine("2. Delete animal from Database");
            Console.WriteLine("3. Show animals");
            Console.WriteLine("4. Add product to Database");
            Console.WriteLine("5. Delete product from Database");
            Console.WriteLine("6. Show products");
            Console.WriteLine("7. Show users");
            Console.WriteLine("8. Ban user");
            Console.WriteLine("9. Logout user");
            
            Console.WriteLine("Enter a number: ");
            if (!int.TryParse(Console.ReadLine(), out var number)) return;
            switch (number)
            {
                case 1: saView.ShowAddAnimalMenu(); break;
                case 2: saView.ShowDeleteAnimalMenu(); break;
                case 3: saView.ShowAllSaleAnimals(); break;
                case 4: productView.ShowAddProductMenu(); break;
                case 5: productView.ShowDeleteProductMenu(); break;
                case 6: productView.ShowProducts(); break;
                case 7: adminView.ShowUsers(); break;
                case 8: adminView.ShowBanClientMenu(); break;
                case 9: authView.Logout();
                    return;
            }
        }
    }
}