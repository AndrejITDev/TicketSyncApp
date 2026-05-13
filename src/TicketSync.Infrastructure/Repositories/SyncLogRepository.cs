using Microsoft.EntityFrameworkCore;
using TicketSync.Core.Interfaces;
using TicketSync.Core.Models;
using TicketSync.Infrastructure.Data;

namespace TicketSync.Infrastructure.Repositories;

public class SyncLogRepository : BaseRepository<SyncLog>, ISyncLogRepository
{
    public SyncLogRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<SyncLog>> GetByTicketMappingIdAsync(int ticketMappingId)
    {
        return await _dbSet
            .Where(s => s.TicketMappingId == ticketMappingId)
            .OrderByDescending(s => s.ExecutedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<SyncLog>> GetFailedLogsAsync()
    {
        return await _dbSet
            .Where(s => s.Status == "FAILED")
            .OrderByDescending(s => s.ExecutedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<SyncLog>> GetLogsBySyncDirectionAsync(string syncDirection)
    {
        return await _dbSet
            .Where(s => s.SyncDirection == syncDirection)
            .OrderByDescending(s => s.ExecutedAt)
            .ToListAsync();
    }
}
