using AnimalLombard.Context.Interfaces;
using AnimalLombard.Modals;
using AnimalLombard.Repository.Interfaces;
using AnimalLombard.Services;
using Moq;
using Xunit;

namespace AnimalLombard.Tests;

public class SaleAnimalServiceTest
{
    private readonly Mock<ISaleAnimalRepository> _mockRepo;
    private readonly Mock<IOrderContext> _mockOrderContext;
    private readonly SaleAnimalService _saleAnimalService;

    public SaleAnimalServiceTest()
    {
        _mockRepo = new Mock<ISaleAnimalRepository>();
        _mockOrderContext = new Mock<IOrderContext>();

        var mockDataStore = new Mock<IDataStore>();
        mockDataStore.Setup(ds => ds.SaleAnimalRepository).Returns(_mockRepo.Object);
        
        _saleAnimalService = new SaleAnimalService(mockDataStore.Object, _mockOrderContext.Object);
    }

    [Fact]
    public void ShowAvailableAnimals_ReturnsPagedResults()
    {
        var animals = Enumerable.Range(1, 25)
            .Select(i => SaleAnimal.Create($"Name{i}", AnimalType.CAT, "Cat", "", 100 + i))
            .ToList();

        _mockRepo.Setup(r => r.FindAllAvailable()).Returns(animals);
        
        var result = _saleAnimalService.ShowAvailableAnimals(page: 1, pageSize: 10);
        
        Assert.Equal(10, result.Count);
        Assert.Equal("Name11", result[0].Name);
    }

    [Fact]
    public void AddAnimal_WithValidData_SavesAnimal()
    {
        var attributes = new Dictionary<string, string> { { "Color", "Brown" } };
        var result = _saleAnimalService.AddAnimal("Bobby", AnimalType.DOG, "Doberman", "Nice dog", 200, attributes);
        
        Assert.True(result);
        _mockRepo.Verify(r => r.Save(It.IsAny<SaleAnimal>()), Times.Once);
    }

    [Fact]
    public void BuyAnimal_ValidAnimal_AddsToOrderAndMarksUnavailable()
    {
        var animal = SaleAnimal.Create("Bobby", AnimalType.DOG, "Doberman", "Nice dog", 200);
        _mockRepo.Setup(r => r.FindById("123")).Returns(animal);
        _mockOrderContext.Setup(c => c.CurrentOrder).Returns((Order)null!);
        
        var result = _saleAnimalService.BuyAnimal("123");
        
        Assert.False(result!.IsAvailable);
        _mockOrderContext.Verify(c => c.StartNewOrder(), Times.Once);
        _mockOrderContext.Verify(c => c.AddSaleAnimalToOrder(animal), Times.Once);
        _mockRepo.Verify(r => r.Save(animal), Times.Once);
    }
    
    [Fact]
    public void DeleteAnimal_WithValidId_DeletesAnimal()
    {
        _saleAnimalService.DeleteAnimal("abc123");
        
        _mockRepo.Verify(r => r.Delete("abc123"), Times.Once);
    }

    [Fact]
    public void DeleteAnimal_WithNull_ThrowsException()
    {
        Assert.Throws<ArgumentNullException>(() => _saleAnimalService.DeleteAnimal(null));
    }
}