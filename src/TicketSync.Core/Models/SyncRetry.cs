namespace TicketSync.Core.Models;

public class SyncRetry
{
    public int Id { get; set; }
    public int TicketMappingId { get; set; }
    public required string SyncDirection { get; set; }
    public int RetryCount { get; set; } = 0;
    public int MaxRetries { get; set; } = 3;
    public DateTime? LastRetryAt { get; set; }
    public DateTime? NextRetryAt { get; set; }
    public string Status { get; set; } = "PENDING"; // PENDING, RETRYING, COMPLETED, FAILED
    public string? ErrorMessage { get; set; }

    // Navigation property
    public TicketMapping? TicketMapping { get; set; }
}
