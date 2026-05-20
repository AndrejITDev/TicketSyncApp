using TicketSync.Core.Models;

namespace TicketSync.Core.Interfaces;

public interface ISyncRetryRepository
{
    Task<SyncRetry?> GetByIdAsync(int id);
    Task<IEnumerable<SyncRetry>> GetAllAsync();
    Task<IEnumerable<SyncRetry>> GetByTicketMappingIdAsync(int ticketMappingId);
    Task<IEnumerable<SyncRetry>> GetPendingRetriesAsync();
    Task<int> AddAsync(SyncRetry syncRetry);
    Task<int> UpdateAsync(SyncRetry syncRetry);
    Task<int> DeleteAsync(int id);
}
