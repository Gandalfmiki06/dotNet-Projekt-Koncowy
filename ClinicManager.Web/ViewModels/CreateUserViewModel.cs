using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class CreateUserViewModel
{
    [Required]
    [Display(Name = "Nazwa użytkownika")]
    public string UserName { get; set; } = "";

    [Required]
    [Display(Name = "Imię i nazwisko")]
    public string FullName { get; set; } = "";

    public string? Email { get; set; }

    [Required]
    [Display(Name = "Hasło")]
    public string Password { get; set; } = "";

    [Required]
    [Display(Name = "Rola")]
    public string Role { get; set; } = "";

    public IEnumerable<SelectListItem> AvailableRoles { get; set; } = [];
}