namespace TicketSync.Core.Models;

public class FieldMappingConfig
{
    public int Id { get; set; }
    public required string JiraFieldName { get; set; }
    public required string AseeFieldName { get; set; }
    public required string FieldType { get; set; } // TEXT, SELECT, DATE, etc.
    public bool IsMappingRequired { get; set; } = false;
    public string? TransformationRule { get; set; } // JSON sa pravilima transformacije
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
