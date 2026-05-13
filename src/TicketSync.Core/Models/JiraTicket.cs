namespace TicketSync.Core.Models;

public class JiraTicket
{
    public required string Key { get; set; }
    public required string Id { get; set; }
    public required string Summary { get; set; }
    public string? Description { get; set; }
    public required string Status { get; set; }
    public required string IssueType { get; set; }
    public string? Priority { get; set; }
    public string? Assignee { get; set; }
    public string? Reporter { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public Dictionary<string, object>? CustomFields { get; set; }
}
