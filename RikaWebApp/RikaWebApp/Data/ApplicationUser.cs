using Microsoft.AspNetCore.Identity;

namespace RikaWebApp.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public string? AddressId { get; set; }

    public UserAddress? UserAddress { get; set; }

    public string? ProfileImageId { get; set; }

    public UserProfileImage? UserProfileImage { get; set; }
}
