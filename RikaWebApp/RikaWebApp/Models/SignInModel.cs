using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.Models;

public class SignInModel
{
    [Display(Name = "Email", Prompt = "Email")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email address is required")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;
    
    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}
