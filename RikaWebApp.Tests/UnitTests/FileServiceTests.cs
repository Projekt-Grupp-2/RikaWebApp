using Microsoft.EntityFrameworkCore;
using RikaWebApp.Data;
using RikaWebApp.Services;

namespace RikaWebApp.Tests.UnitTests;

public class FileServiceTests
{
    private readonly ApplicationDbContext _context;
    private readonly FileService _fileService;

    public FileServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new ApplicationDbContext(options);
        _fileService = new FileService(_context);
    }

    [Fact]
    public async Task CreateFileAsync_ShouldAddNewProfileImage_WhenImageDoesNotExist()
    {
        // Arrange
        var filePath = "test/path/to/image.jpg";

        // Act
        var result = await _fileService.CreateFileAsync(filePath);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(filePath, result.ProfileImage);

        // Verify that the entity was saved in the database
        var savedImage = await _context.UserProfileImage.FirstOrDefaultAsync(x => x.ProfileImage == filePath);
        Assert.NotNull(savedImage);
        Assert.Equal(filePath, savedImage.ProfileImage);
    }

    [Fact]
    public async Task CreateFileAsync_ShouldReturnExistingProfileImage_WhenImageAlreadyExists()
    {
        // Arrange
        var filePath = "test/path/to/existingImage.jpg";
        var existingImage = new UserProfileImage { ProfileImage = filePath };
        await _context.UserProfileImage.AddAsync(existingImage);
        await _context.SaveChangesAsync();

        // Act
        var result = await _fileService.CreateFileAsync(filePath);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingImage.Id, result.Id);
    }

    [Fact]
    public async Task FindProfileImageAsync_ShouldReturnProfileImage_WhenIdExists()
    {
        // Arrange
        var profileImage = new UserProfileImage { ProfileImage = "test/path/to/image2.jpg" };
        await _context.UserProfileImage.AddAsync(profileImage);
        await _context.SaveChangesAsync();

        // Act
        var result = await _fileService.FindProfileImageAsync(profileImage.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(profileImage.Id, result.Id);
    }

    [Fact]
    public async Task FindProfileImageAsync_ShouldReturnNull_WhenIdDoesNotExist()
    {
        // Arrange
        var nonExistentId = "non-existent-id";

        // Act
        var result = await _fileService.FindProfileImageAsync(nonExistentId);

        // Assert
        Assert.Null(result);
    }
}
