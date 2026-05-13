namespace TicketSync.Core.Models;

public class AseeTicket
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string Status { get; set; }
    public string? Priority { get; set; }
    public string? Assignee { get; set; }
    public string? Reporter { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public Dictionary<string, object>? CustomFields { get; set; }
}
