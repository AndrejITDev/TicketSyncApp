namespace TicketSync.Core.Models;

public class JiraTicket
{
    public string Key { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = string.Empty;
    public string IssueType { get; set; } = string.Empty;
    public string? Priority { get; set; }
    public string? Assignee { get; set; }
    public string? Reporter { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public Dictionary<string, object>? CustomFields { get; set; }
}
