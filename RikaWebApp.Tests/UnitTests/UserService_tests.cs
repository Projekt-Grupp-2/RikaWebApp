using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RikaWebApp.Data;
using RikaWebApp.Services;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace RikaWebApp.Tests.UnitTests
{
    public class UserServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<AuthenticationStateProvider> _mockAuthenticationStateProvider;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                null, null, null, null, null, null, null, null);

            _mockAuthenticationStateProvider = new Mock<AuthenticationStateProvider>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase("TestDb")
                            .Options;
            _dbContext = new ApplicationDbContext(options);

            _userService = new UserService(
                _mockUserManager.Object,
                _mockAuthenticationStateProvider.Object,
                _dbContext
            );
        }

        [Fact]
        public async Task GetUserAsync_ShouldReturnUser_WhenAuthenticated()
        {
            // Arrange
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "testUser"),
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "Test");

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var authenticationState = new AuthenticationState(claimsPrincipal);
            _mockAuthenticationStateProvider.Setup(x => x.GetAuthenticationStateAsync())
                .ReturnsAsync(authenticationState);

            var applicationUser = new ApplicationUser { UserName = "testUser", Id = "1" };
            _mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(applicationUser);

            // Act
            var result = await _userService.GetUserAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("testUser", result.UserName);
        }

        [Fact]
        public async Task GetUserAsync_ShouldReturnNull_WhenNotAuthenticated()
        {
            // Arrange
            var claimsPrincipal = new ClaimsPrincipal();
            var authenticationState = new AuthenticationState(claimsPrincipal);
            _mockAuthenticationStateProvider.Setup(x => x.GetAuthenticationStateAsync())
                .ReturnsAsync(authenticationState);

            // Act
            var result = await _userService.GetUserAsync();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task IsExternalLoginAsync_ShouldReturnTrue_WhenUserHasExternalLogins()
        {
            // Arrange
            var user = new ApplicationUser { UserName = "testUser" };
            var externalLogins = new List<UserLoginInfo> { new UserLoginInfo("provider", "key", "displayName") };
            _mockUserManager.Setup(x => x.GetLoginsAsync(user))
                .ReturnsAsync(externalLogins);

            // Act
            var result = await _userService.IsExternalLoginAsync(user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsExternalLoginAsync_ShouldReturnFalse_WhenUserHasNoExternalLogins()
        {
            // Arrange
            var user = new ApplicationUser { UserName = "testUser" };
            var externalLogins = new List<UserLoginInfo>();
            _mockUserManager.Setup(x => x.GetLoginsAsync(user))
                .ReturnsAsync(externalLogins);

            // Act
            var result = await _userService.IsExternalLoginAsync(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldUpdateUser_WhenValidUser()
        {
            // Arrange
            var existingUser = new ApplicationUser { Id = "1", UserName = "testUser", Email = "test@example.com" };
            var updatedUser = new ApplicationUser { Id = "1", UserName = "newUser", Email = "new@example.com", Name = "New Name" };

            var claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "testUser")
            }, "Test");

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var authenticationState = new AuthenticationState(claimsPrincipal);
            _mockAuthenticationStateProvider.Setup(x => x.GetAuthenticationStateAsync())
                                             .ReturnsAsync(authenticationState);


            _mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                            .ReturnsAsync(existingUser);


            _mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
                            .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userService.UpdateUserAsync(updatedUser);

            // Assert
            Assert.True(result.Succeeded);
            _mockUserManager.Verify(x => x.UpdateAsync(It.IsAny<ApplicationUser>()), Times.Once);
        }
    }
}
