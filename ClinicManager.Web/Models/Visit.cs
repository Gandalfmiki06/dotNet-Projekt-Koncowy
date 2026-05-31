public class Visit
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public VisitStatus Status { get; set; }

    // FK
    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    public string DoctorId { get; set; }
    public ApplicationUser Doctor { get; set; }

    public List<ClinicalNote> Notes { get; set; } = new();
}