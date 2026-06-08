using System.ComponentModel.DataAnnotations;

public class ChangePasswordViewModel
{
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Aktualne hasło")]
    public string CurrentPassword { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Nowe hasło")]
    public string NewPassword { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Potwierdź hasło")]
    [Compare(nameof(NewPassword), ErrorMessage = "Hasła nie są takie same.")]
    public string ConfirmPassword { get; set; } = "";
}