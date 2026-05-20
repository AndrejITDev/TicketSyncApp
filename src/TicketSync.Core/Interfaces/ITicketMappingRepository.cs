using TicketSync.Core.Models;

namespace TicketSync.Core.Interfaces;

public interface ITicketMappingRepository
{
    Task<TicketMapping?> GetByIdAsync(int id);
    Task<TicketMapping?> GetByJiraKeyAsync(string jiraKey);
    Task<TicketMapping?> GetByAseeIdAsync(string aseeId);
    Task<IEnumerable<TicketMapping>> GetAllAsync();
    Task<IEnumerable<TicketMapping>> GetActiveTicketsAsync();
    Task<IEnumerable<TicketMapping>> GetByStatusAsync(string status);
    Task<int> AddAsync(TicketMapping ticketMapping);
    Task<int> UpdateAsync(TicketMapping ticketMapping);
    Task<int> DeleteAsync(int id);
}
