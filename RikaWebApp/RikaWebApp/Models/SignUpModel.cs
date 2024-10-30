using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.Models;

public class SignUpModel
{
    [Display(Name = "User name", Prompt = "Enter your name", Order = 0)]
    [Required(ErrorMessage = "Name is required")]
    [MinLength(2, ErrorMessage = "The name should be min 2 symbol")]
    [DataType(DataType.Text)]
    public string Name { get; set; } = null!;
  


    [Display(Name = "Email address", Prompt = "Enter your email address", Order = 1)]
    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;

    
    [Display(Name = "Password", Prompt = "Enter your password", Order = 2)]
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_\-+=])[A-Za-z\d!@#$%^&*()_\-+=]{8,}$", ErrorMessage = "Invalid password")]

    public string Password { get; set; } = null!;


    [Display(Name = "Confirm password", Prompt = "Confirm your password", Order = 4)]
    [Required(ErrorMessage = "You whould confirm you password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Password does not match")]
    public string ConfirmPassword { get; set; } = null!;

    [Display(Name = "I agree to the Term & Conditions", Order = 5)]
    [CheckBoxRequired(ErrorMessage = "You must agree with terms & conditions")]

    public bool TermsAndCondition { get; set; } = false;
}

public class CheckBoxRequired : ValidationAttribute
{
    public override bool IsValid(object? value) => value is bool b && b;
}

