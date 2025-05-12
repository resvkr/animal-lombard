using AnimaLombart.Repository;
using Models;

namespace Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;
    public ProductService(DataStore dataStore)
    {
        _productRepository = dataStore.ProductRepository;
    }
    public void AddProduct(Product product)
    {
        _productRepository.Save(product);
    }
    public void UpdateProduct(Product product)
    {
        _productRepository.Save(product);
    }
    public void DeleteProduct(string productId)
    {
        _productRepository.Delete(productId);
    }
}