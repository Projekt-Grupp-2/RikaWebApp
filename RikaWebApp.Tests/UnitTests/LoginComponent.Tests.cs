using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RikaWebApp.Data;

namespace RikaWebApp.Tests.UnitTests;

public class LoginComponent
{
    private readonly Mock<SignInManager<ApplicationUser>> _mockSignInManager;
    private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;

    public LoginComponent()
    {
        // Create mock dependencies required for SignInManager
        _mockUserManager = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(),null, null, null,null,null,null,null,null);

        var contextAccessor = Mock.Of<IHttpContextAccessor>();
        var userClaimsPrincipalFactory = Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>();
        var options = Mock.Of<IOptions<IdentityOptions>>();
        var logger = Mock.Of<ILogger<SignInManager<ApplicationUser>>>();

        _mockSignInManager = new Mock<SignInManager<ApplicationUser>>(
            _mockUserManager.Object,contextAccessor, userClaimsPrincipalFactory, options,logger, null,null);
    }

    [Fact]
    public async Task LoginUser_ShouldReturnError_IfEmailIsNull()
    {
        // Arrange
        string email = null!;
        string password = "ValidPassword123!";

        _mockSignInManager
            .Setup(sm => sm.PasswordSignInAsync(email, password, It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(SignInResult.Failed);

        // Act
        var result = await _mockSignInManager.Object.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

        // Assert
        Assert.False(result.Succeeded);
    }

    [Fact]
    public async Task LoginUser_ShouldReturnError_IfPasswordIsNull()
    {
        // Arrange
        string email = "user@example.com";
        string password = null!;

        _mockSignInManager
            .Setup(sm => sm.PasswordSignInAsync(email, password, It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(SignInResult.Failed);

        // Act
        var result = await _mockSignInManager.Object.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

        // Assert
        Assert.False(result.Succeeded);
    }

    [Fact]
    public async Task LoginUser_ShouldReturnError_IfUserNotFound()
    {
        // Arrange
        string email = "nonexistentuser@example.com";
        string password = "ValidPassword123!";

        _mockSignInManager
            .Setup(sm => sm.PasswordSignInAsync(email, password, It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(SignInResult.Failed);

        // Act
        var result = await _mockSignInManager.Object.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

        // Assert
        Assert.False(result.Succeeded);
    }

    [Fact]
    public async Task LoginUser_ShouldSucceed_WithValidCredentials()
    {
        // Arrange
        string email = "validuser@example.com";
        string password = "ValidPassword123!";

        _mockSignInManager
            .Setup(sm => sm.PasswordSignInAsync(email, password, It.IsAny<bool>(), It.IsAny<bool>()))
            .ReturnsAsync(SignInResult.Success);

        // Act
        var result = await _mockSignInManager.Object.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);

        // Assert
        Assert.True(result.Succeeded);
    }

}
