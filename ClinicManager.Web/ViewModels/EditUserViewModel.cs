using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class EditUserViewModel
{
    public string Id { get; set; } = "";

    [Required]
    [Display(Name = "Nazwa użytkownika")]
    public string UserName { get; set; } = "";

    [Required]
    [Display(Name = "Imię i nazwisko")]
    public string FullName { get; set; } = "";

    public string? Email { get; set; }

    [Required]
    [Display(Name = "Rola")]
    public string Role { get; set; } = "";

    public IEnumerable<SelectListItem> AvailableRoles { get; set; } = [];
}