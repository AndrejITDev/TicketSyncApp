namespace TicketSync.Core.Models;

public class SyncLog
{
    public int Id { get; set; }
    public int TicketMappingId { get; set; }
    public required string SyncDirection { get; set; } // JIRA_TO_ASEE, ASEE_TO_JIRA
    public required string ActionType { get; set; } // CREATE, UPDATE, CLOSE, etc.
    public required string SourceSystem { get; set; } // JIRA, ASEE
    public required string TargetSystem { get; set; }
    public string? Details { get; set; } // JSON sa detaljima promene
    public string Status { get; set; } = "SUCCESS"; // SUCCESS, FAILED, PENDING
    public string? ErrorMessage { get; set; }
    public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public TicketMapping? TicketMapping { get; set; }
}
