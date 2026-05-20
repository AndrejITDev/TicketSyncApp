using TicketSync.Core.Models;

namespace TicketSync.Core.Interfaces;

public interface ISyncLogRepository
{
    Task<SyncLog?> GetByIdAsync(int id);
    Task<IEnumerable<SyncLog>> GetAllAsync();
    Task<IEnumerable<SyncLog>> GetByTicketMappingIdAsync(int ticketMappingId);
    Task<IEnumerable<SyncLog>> GetFailedLogsAsync();
    Task<IEnumerable<SyncLog>> GetLogsBySyncDirectionAsync(string syncDirection);
    Task<int> AddAsync(SyncLog syncLog);
    Task<int> UpdateAsync(SyncLog syncLog);
    Task<int> DeleteAsync(int id);
}
