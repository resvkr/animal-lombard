using System.ComponentModel.DataAnnotations;
using AnimalLombard.Context.Interfaces;
using AnimalLombard.Repository;
using AnimalLombard.Modals;
using AnimalLombard.Repository.Interfaces;

namespace AnimalLombard.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IOrderContext _orderContext;
    public ProductService(DataStore dataStore, IOrderContext orderContext)
    {
        _productRepository = dataStore.ProductRepository;
        _orderContext = orderContext;
    }

    public List<Product> GetProducts(int page = 0, int pageSize = 10)
    {
        if (page < 0) throw new ArgumentOutOfRangeException(nameof(page), "Page must be >= 0");
        if (pageSize <= 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "PageSize must be > 0");
        
        var products = _productRepository.FindAll();
        return products
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public Product? BuyProduct(string id)
    {
        var product = _productRepository.FindById(id);
        if (product is null) return null;

        var order = _orderContext.CurrentOrder;

        try
        {
            if (order is null) _orderContext.StartNewOrder();
            _orderContext.AddProductToOrder(product);
            _productRepository.Delete(id);
            return product;
        } 
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    public void AddProduct(Product product)
    {
        _productRepository.Save(product);
    }

    public bool AddProduct(string? name, decimal price, string? description)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description) || price <= 0)
        {
            Console.WriteLine("Invalid input, please try again");
            return false;
        }

        try
        {
            var product = Product.Create(name, price, description);
            _productRepository.Save(product);
            return true;
        }
        catch (ValidationException ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
    public void UpdateProduct(Product product)
    {
        _productRepository.Save(product);
    }
    public void DeleteProduct(string? productId)
    {
        if (string.IsNullOrEmpty(productId))
            throw new ArgumentNullException(nameof(productId));
        
        _productRepository.Delete(productId);
    }
}