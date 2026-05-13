using TicketSync.Core.Models;

namespace TicketSync.Core.Interfaces;

public interface ITicketMappingRepository : IRepository<TicketMapping>
{
    Task<TicketMapping?> GetByJiraKeyAsync(string jiraKey);
    Task<TicketMapping?> GetByAseeIdAsync(string aseeId);
    Task<IEnumerable<TicketMapping>> GetActiveTicketsAsync();
    Task<IEnumerable<TicketMapping>> GetByStatusAsync(string status);
}
