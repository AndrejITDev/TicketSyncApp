namespace TicketSync.Core.Models;

public class TicketFieldSnapshot
{
    public int Id { get; set; }
    public int TicketMappingId { get; set; }
    public required string SnapshotSystem { get; set; } // JIRA, ASEE
    public required string FieldName { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public TicketMapping? TicketMapping { get; set; }
}
