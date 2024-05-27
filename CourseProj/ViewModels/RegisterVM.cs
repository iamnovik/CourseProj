using System.ComponentModel.DataAnnotations;

namespace CourseProj.ViewModels;

public class RegisterVm
{
    [Required]
    public string? Name { get; set; }
    [Required]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords don't match.")]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }
    
    [Display(Name = "Admin")] public bool AdminRole { get; set; } = false;

}