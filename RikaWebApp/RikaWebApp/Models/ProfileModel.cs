using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.Models;

public class ProfileModel
{
    [DataType(DataType.ImageUrl)]
    public string? ProfileImg { get; set; }

    
    [DataType(DataType.Text)]
    public string Name { get; set; } = null!;
      
    
    [EmailAddress]
    public string Email { get; set; } = null!;
    
}
