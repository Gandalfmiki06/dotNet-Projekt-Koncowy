public class ClinicalNote
{
    public int Id { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public int VisitId { get; set; }
    public Visit Visit { get; set; }
}