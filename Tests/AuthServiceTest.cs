using AnimalLombard.Context.Interfaces;
using AnimalLombard.Modals;
using AnimalLombard.Repository.Interfaces;
using AnimalLombard.Services;
using AnimalLombard.Utils;
using Moq;
using Xunit;

namespace AnimalLombard.Tests;

public class AuthServiceTest
{
    private readonly Mock<IUserRepository> _mockUserRepo;
    private readonly Mock<IMutableUserContext> _mockUserContext;
    private readonly AuthService _authService;

    public AuthServiceTest()
    {
        _mockUserRepo = new Mock<IUserRepository>();
        _mockUserContext = new Mock<IMutableUserContext>();
        
        var mockDataStore = new Mock<IDataStore>();
        mockDataStore.Setup(ds => ds.UserRepository).Returns(_mockUserRepo.Object);
        
        _authService = new AuthService(mockDataStore.Object, _mockUserContext.Object);
    }

    [Fact]
    public void Login_ValidCredentials_ReturnsUser()
    {
        var user = User.Create("Test", "+48123567890", "test@email.com", HashUtils.HashPassword("pass"));

        _mockUserRepo.Setup(r => r.FindByEmail("test@email.com")).Returns(user);

        var result = _authService.Login("test@email.com", "pass");
        
        Assert.Equal(user, result);
        _mockUserContext.Verify(uc => uc.SetCurrentUser(user), Times.Once);
    }

    [Fact]
    public void Login_UserNotFound_ThrowsException()
    {
        _mockUserRepo.Setup(r => r.FindByEmail("x@y.com")).Returns(((User?)null)!);

        Assert.Throws<ArgumentException>(() => _authService.Login("x@y.com", "pass"));
    }

    [Fact]
    public void Login_InvalidPassword_ThrowsException()
    {
        var user = User.Create("Test", "+48123567890", "x@y.com", HashUtils.HashPassword("right"));
        _mockUserRepo.Setup(r => r.FindByEmail("x@y.com")).Returns(user);

        Assert.Throws<ArgumentException>(() => _authService.Login("x@y.com", "wrong"));
    }
    
    [Fact]
    public void Login_UserBanned_ThrowsException()
    {
        var user = User.Create("Test", "+4812345689", "x@y.com", HashUtils.HashPassword("pass"));
        user.IsActiveProfile = false;

        _mockUserRepo.Setup(r => r.FindByEmail("x@y.com")).Returns(user);

        Assert.Throws<ArgumentException>(() => _authService.Login("x@y.com", "pass"));
    }
    
    [Fact]
    public void Register_NewUser_SavesUser()
    {
        _mockUserRepo.Setup(r => r.FindByEmail("new@mail.com")).Returns((User?)null);

        _authService.Register("Name", "+4812345689", "new@mail.com", "password");

        _mockUserRepo.Verify(r => r.Save(It.Is<User>(u =>
            u.Email == "new@mail.com" &&
            HashUtils.VerifyPassword("password", u.PasswordHash)
        )), Times.Once);
    }
    
    [Fact]
    public void Register_ExistingUser_ThrowsException()
    {
        var user = User.Create("Test", "+4812345689", "exist@mail.com", "hash");
        _mockUserRepo.Setup(r => r.FindByEmail("exist@mail.com")).Returns(user);

        Assert.Throws<ArgumentException>(() =>
            _authService.Register("Name", "+4812345689", "exist@mail.com", "password")
        );
    }
    
    [Fact]
    public void Logout_ClearsUserContext()
    {
        _authService.Logout();
        _mockUserContext.Verify(c => c.SetCurrentUser(null), Times.Once);
    }
}