using Microsoft.EntityFrameworkCore;

[Index(nameof(Date), nameof(DoctorId))]
public class Visit
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public VisitStatus Status { get; set; }

    // FK
    public int PatientId { get; set; }
    public required Patient Patient { get; set; }

    public required string DoctorId { get; set; }
    public required ApplicationUser Doctor { get; set; }

    public List<ClinicalNote> Notes { get; set; } = new();
}