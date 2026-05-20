namespace TicketSync.Core.Models;

public class SyncRetry
{
    public int Id { get; set; }
    public int TicketMappingId { get; set; }
    public string SyncDirection { get; set; } = string.Empty;
    public int RetryCount { get; set; }
    public int MaxRetries { get; set; }
    public DateTime? LastRetryAt { get; set; }
    public DateTime? NextRetryAt { get; set; }
    public string Status { get; set; } = string.Empty; // PENDING, RETRYING, COMPLETED, FAILED
    public string? ErrorMessage { get; set; }
}
