namespace TicketSync.Core.Models;

public class TicketMapping
{
    public int Id { get; set; }
    public required string JiraTicketKey { get; set; }
    public required string JiraTicketId { get; set; }
    public required string AseeTicketId { get; set; }
    public string SyncStatus { get; set; } = "ACTIVE"; // ACTIVE, CLOSED, PAUSED
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastSyncedAt { get; set; }

    // Navigation properties
    public ICollection<SyncLog> SyncLogs { get; set; } = new List<SyncLog>();
    public ICollection<TicketFieldSnapshot> FieldSnapshots { get; set; } = new List<TicketFieldSnapshot>();
    public ICollection<SyncRetry> SyncRetries { get; set; } = new List<SyncRetry>();
}
