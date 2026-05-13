using TicketSync.Core.Models;

namespace TicketSync.Core.Interfaces;

public interface IJiraService
{
    Task<JiraTicket?> GetTicketAsync(string ticketKey);
    Task<IEnumerable<JiraTicket>> GetTicketsByJqlAsync(string jql);
    Task<JiraTicket> CreateTicketAsync(JiraTicket ticket);
    Task<JiraTicket> UpdateTicketAsync(string ticketKey, JiraTicket ticket);
    Task<bool> TransitionTicketAsync(string ticketKey, string transitionName);
    Task<IEnumerable<object>> GetTicketChangesAsync(string ticketKey, DateTime since);
}
