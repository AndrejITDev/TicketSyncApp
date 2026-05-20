namespace TicketSync.Core.Models;

public class SyncLog
{
    public int Id { get; set; }
    public int TicketMappingId { get; set; }
    public string SyncDirection { get; set; } = string.Empty; // JIRA_TO_ASEE, ASEE_TO_JIRA
    public string ActionType { get; set; } = string.Empty; // CREATE, UPDATE, CLOSE, etc.
    public string SourceSystem { get; set; } = string.Empty; // JIRA, ASEE
    public string TargetSystem { get; set; } = string.Empty;
    public string? Details { get; set; } // JSON sa detaljima promene
    public string Status { get; set; } = "SUCCESS"; // SUCCESS, FAILED, PENDING
    public string? ErrorMessage { get; set; }
    public DateTime ExecutedAt { get; set; }
}
