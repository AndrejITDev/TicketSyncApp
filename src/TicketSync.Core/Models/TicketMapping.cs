namespace TicketSync.Core.Models;

public class TicketMapping
{
    public int Id { get; set; }
    public string JiraTicketKey { get; set; } = string.Empty;
    public string JiraTicketId { get; set; } = string.Empty;
    public string AseeTicketId { get; set; } = string.Empty;
    public string SyncStatus { get; set; } = "ACTIVE"; // ACTIVE, CLOSED, PAUSED
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? LastSyncedAt { get; set; }
}
