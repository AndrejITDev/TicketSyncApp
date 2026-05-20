namespace TicketSync.Core.Models;

public class FieldMappingConfig
{
    public int Id { get; set; }
    public string JiraFieldName { get; set; } = string.Empty;
    public string AseeFieldName { get; set; } = string.Empty;
    public string FieldType { get; set; } = string.Empty; // TEXT, SELECT, DATE, etc.
    public bool IsMappingRequired { get; set; }
    public string? TransformationRule { get; set; } // JSON sa pravilima transformacije
    public DateTime CreatedAt { get; set; }
}
