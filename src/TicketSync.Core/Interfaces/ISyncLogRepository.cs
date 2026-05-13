using TicketSync.Core.Models;

namespace TicketSync.Core.Interfaces;

public interface ISyncLogRepository : IRepository<SyncLog>
{
    Task<IEnumerable<SyncLog>> GetByTicketMappingIdAsync(int ticketMappingId);
    Task<IEnumerable<SyncLog>> GetFailedLogsAsync();
    Task<IEnumerable<SyncLog>> GetLogsBySyncDirectionAsync(string syncDirection);
}
