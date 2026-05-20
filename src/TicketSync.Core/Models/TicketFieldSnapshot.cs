namespace TicketSync.Core.Models;

public class TicketFieldSnapshot
{
    public int Id { get; set; }
    public int TicketMappingId { get; set; }
    public string SnapshotSystem { get; set; } = string.Empty; // JIRA, ASEE
    public string FieldName { get; set; } = string.Empty;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime ChangedAt { get; set; }
}
