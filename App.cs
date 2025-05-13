using AnimalLombard.Context;
using AnimalLombard.Repository;
using AnimalLombard.Services;
using AnimalLombard.View;

namespace AnimalLombard;

public class App
{
    public void Run()
    {
        var userContext = new UserContext();
        var orderContext = new OrderContext(userContext);

        var userRepository = new UserRepository();
        var saleAnimalRepository = new SaleAnimalRepository();
        var productRepository = new ProductRepository();
        var orderRepository = new OrderRepository();
        var boardingFormRepository = new BoardingFormRepository();
        var boardedAnimalRepository = new BoardedAnimalRepository();
        
        var dataStore = new DataStore(
            userRepository,
            saleAnimalRepository,
            productRepository,
            orderRepository,
            boardingFormRepository,
            boardedAnimalRepository
        );

        var adminService = new AdminService(dataStore);
        var authService = new AuthService(dataStore, userContext);
        var boardedAnimalService = new BoardedAnimalService(dataStore, userContext);
        var boardingFormService = new BoardingFormService(dataStore);
        var orderService = new OrderService(dataStore, orderContext);
        var productService = new ProductService(dataStore, orderContext);
        var saleAnimalService = new SaleAnimalService(dataStore, orderContext);

        var authView = new AuthView(authService);
        var boardedAnimalView = new BoardedAnimalView(boardedAnimalService, boardingFormService);
        var orderView = new OrderView(orderService);
        var productView = new ProductView(productService);
        var saleAnimalView = new SaleAnimalView(saleAnimalService);
        var mainView = new MainView(
            userContext,
            saleAnimalView,
            authView,
            productView,
            orderView,
            boardedAnimalView
        );
        
        mainView.ShowMainMenu();
    }
}