using TicketSync.Core.Models;

namespace TicketSync.Core.Interfaces;

public interface ITicketFieldSnapshotRepository
{
    Task<TicketFieldSnapshot?> GetByIdAsync(int id);
    Task<IEnumerable<TicketFieldSnapshot>> GetAllAsync();
    Task<IEnumerable<TicketFieldSnapshot>> GetByTicketMappingIdAsync(int ticketMappingId);
    Task<int> AddAsync(TicketFieldSnapshot snapshot);
    Task<int> UpdateAsync(TicketFieldSnapshot snapshot);
    Task<int> DeleteAsync(int id);
}
