public class ClinicalNote
{
    public int Id { get; set; }

    public required string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public int VisitId { get; set; }
    public required Visit Visit { get; set; } 
}