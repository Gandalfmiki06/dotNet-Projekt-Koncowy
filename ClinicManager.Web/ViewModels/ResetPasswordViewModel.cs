using System.ComponentModel.DataAnnotations;

public class ResetPasswordViewModel
{
    public string UserId { get; set; } = "";

    public string UserName { get; set; } = "";

    [Required]
    [Display(Name = "Nowe hasło")]
    public string NewPassword { get; set; } = "";
}