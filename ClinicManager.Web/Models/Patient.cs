using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Pesel))]
public class Patient
{
    public int Id { get; set; }

    [Required]
    public required string Pesel { get; set; }

    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    public List<Visit> Visits { get; set; } = new();
}