using System;
using System.Threading.Tasks;
using Moq;
using webapi.BLL.Helpers;
using webapi.BLL.Models;
using webapi.BLL.Repos.Interfaces;
using webapi.BLL.Services.Implementations;
using webapi.DAL.Entities.Main;
using webapi.DAL.Entities.Support;
using Xunit;

public class UserServiceTests
{
    private readonly Mock<IUserRepo> _userRepoMock;
    private readonly JwtSettings _jwtSettings;
    private readonly UserService _userService;
    public UserServiceTests()
    {
        _userRepoMock = new Mock<IUserRepo>();
        _jwtSettings = new JwtSettings
        {
            Key = "s8WULSV3nKaNWt4&2J$m@k2sT!V%NqR5",
            Issuer = "SportBlaze",
            Audience = "grgrs",
            TokenExpirationInMonths = 1
        };
        _userService = new UserService(_userRepoMock.Object, _jwtSettings);
    }

    [Fact]
    public async Task LoginAsync_ReturnsJwtToken_WhenCredentialsAreValid()
    {
        var email = "test@example.com";
        var password = "TestPassword";
        var hashedPassword = "c8b102d45db1b1fd3ad27221d940eae56dc618ad9eaf32334711baa9ba0e05be";
        var salt = "Salt";
        var loginModel = new LoginModel
        {
            Email = email,
            Password = password
        };
        var user = new User
        {
            FirstName = "John",
            Email = email,
            HashedPassword = hashedPassword,
            Salt = salt,
            Role = new Role { Name = "RegisteredUser" }
        };
        _userRepoMock.Setup(repo => repo.GetRegisteredUser(email)).ReturnsAsync(user);
        var jwtCreator = new JwtCreator(_jwtSettings);
        var expectedJwtToken = jwtCreator.GenerateJwtToken(email, user.Role.Name, user.FirstName);
        var result = await _userService.LoginAsync(loginModel);
        Assert.Equal(expectedJwtToken, result);
    }
    [Fact]
    public async Task LoginAsync_ReturnsNull_WhenCredentialsAreInvalid()
    {
        var email = "test@example.com";
        var password = "TestPassword";
        var loginModel = new LoginModel
        {
            Email = email,
            Password = password
        };
        _userRepoMock.Setup(repo => repo.GetRegisteredUser(email)).ReturnsAsync((User)null);
        var result = await _userService.LoginAsync(loginModel);
        Assert.Null(result);
    }
    [Fact]
    public async Task UpdateUserAsync_ReturnsTrue_WhenUserIsUpdatedSuccessfully()
    {
        var userModel = new UserModel
        {
            FirstName = "John",
            LastName = "Doe",
            Phone = "1234567890",
            City = "New York",
            Address = "123 Main St"
        };
        var email = "test@example.com";
        var user = new User
        {
            Email = email,
            FirstName = "OldFirstName",
            LastName = "OldLastName",
            Phone = "OldPhone",
            City = "OldCity",
            Address = "OldAddress"
        };
        _userRepoMock.Setup(repo => repo.GetRegisteredUser(email)).ReturnsAsync(user);
        _userRepoMock
            .Setup(repo => repo.UpdateUser(It.IsAny<User>()))
            .ReturnsAsync(user);
        var result = await _userService.UpdateUserAsync(userModel, email);
        Assert.True(result);
        Assert.Equal(userModel.FirstName, user.FirstName);
        Assert.Equal(userModel.LastName, user.LastName);
        Assert.Equal(userModel.Phone, user.Phone);
        Assert.Equal(userModel.City, user.City);
        Assert.Equal(userModel.Address, user.Address);
    }

    [Fact]
    public async Task UpdateUserAsync_ReturnsFalse_WhenUserDoesNotExist()
    {
        var userModel = new UserModel
        {
            FirstName = "John",
            LastName = "Doe",
            Phone = "1234567890",
            City = "New York",
            Address = "123 Main" +
            " St"
        };
        var email = "test@example.com";
        _userRepoMock.Setup(repo => repo.GetRegisteredUser(email)).ReturnsAsync((User)null);
        var result = await _userService.UpdateUserAsync(userModel, email);
        Assert.False(result);
    }
}
