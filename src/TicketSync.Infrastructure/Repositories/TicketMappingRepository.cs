using Microsoft.EntityFrameworkCore;
using TicketSync.Core.Interfaces;
using TicketSync.Core.Models;
using TicketSync.Infrastructure.Data;

namespace TicketSync.Infrastructure.Repositories;

public class TicketMappingRepository : BaseRepository<TicketMapping>, ITicketMappingRepository
{
    public TicketMappingRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<TicketMapping?> GetByJiraKeyAsync(string jiraKey)
    {
        return await _dbSet.FirstOrDefaultAsync(t => t.JiraTicketKey == jiraKey);
    }

    public async Task<TicketMapping?> GetByAseeIdAsync(string aseeId)
    {
        return await _dbSet.FirstOrDefaultAsync(t => t.AseeTicketId == aseeId);
    }

    public async Task<IEnumerable<TicketMapping>> GetActiveTicketsAsync()
    {
        return await _dbSet
            .Where(t => t.SyncStatus == "ACTIVE")
            .ToListAsync();
    }

    public async Task<IEnumerable<TicketMapping>> GetByStatusAsync(string status)
    {
        return await _dbSet
            .Where(t => t.SyncStatus == status)
            .ToListAsync();
    }
}
