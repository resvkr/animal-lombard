using AnimalLombard.Services;

namespace AnimalLombard.View;

public class ProductView
{
    private readonly ProductService _productService;

    public ProductView(ProductService productService)
    {
        _productService = productService;
    }

    public void ShowProducts()
    {
        var currentPage = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("List of available products for sale: (For exit press 'q')");
            var products = _productService.GetProducts(currentPage);

            products.ForEach(Console.WriteLine);

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

    public void ShowBuyProductMenu()
    {
        Console.WriteLine("Provide id of the product you want to buy: ");
        var id = Console.ReadLine();
        if (id is null)
        {
            Console.WriteLine("Invalid input, please try again");
            return;
        }

        var product = _productService.BuyProduct(id);

        if (product is null)
        {
            Console.WriteLine("Product not found or not available for sale");
            return;
        }

        Console.WriteLine(product.Name + " was added to your order");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public void ShowAddProductMenu()
    {
        Console.WriteLine("Provide name of the product: ");
        var name = Console.ReadLine();

        Console.WriteLine("Provide price of the product: ");
        var price = decimal.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine("You can also provide description of the product: ");
        var description = Console.ReadLine();

        var success = _productService.AddProduct(name, price, description);
        Console.WriteLine(success ? "Product added successfully" : "Product not added");
    }

    public void ShowDeleteProductMenu()
    {
        try
        {
            Console.WriteLine("Provide id of the product you want to delete: ");
            var id = Console.ReadLine();

            _productService.DeleteProduct(id);
            Console.WriteLine("Product deleted successfully");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}