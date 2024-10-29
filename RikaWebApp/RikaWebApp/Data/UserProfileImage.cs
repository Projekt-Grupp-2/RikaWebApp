using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.Data;

public class UserProfileImage
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string ProfileImage { get; set; } = null!;

}
