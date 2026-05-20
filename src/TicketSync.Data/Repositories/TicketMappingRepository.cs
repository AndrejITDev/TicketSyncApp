using Dapper;
using System.Data;
using TicketSync.Core.Interfaces;
using TicketSync.Core.Models;

namespace TicketSync.Data.Repositories;

public class TicketMappingRepository : ITicketMappingRepository
{
    private readonly DapperContext _context;

    public TicketMappingRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<TicketMapping?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.TicketMappings WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<TicketMapping>(query, new { Id = id });
    }

    public async Task<TicketMapping?> GetByJiraKeyAsync(string jiraKey)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.TicketMappings WHERE JiraTicketKey = @JiraTicketKey";
        return await connection.QueryFirstOrDefaultAsync<TicketMapping>(query, new { JiraTicketKey = jiraKey });
    }

    public async Task<TicketMapping?> GetByAseeIdAsync(string aseeId)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.TicketMappings WHERE AseeTicketId = @AseeTicketId";
        return await connection.QueryFirstOrDefaultAsync<TicketMapping>(query, new { AseeTicketId = aseeId });
    }

    public async Task<IEnumerable<TicketMapping>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.TicketMappings ORDER BY CreatedAt DESC";
        return await connection.QueryAsync<TicketMapping>(query);
    }

    public async Task<IEnumerable<TicketMapping>> GetActiveTicketsAsync()
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.TicketMappings WHERE SyncStatus = 'ACTIVE' ORDER BY CreatedAt DESC";
        return await connection.QueryAsync<TicketMapping>(query);
    }

    public async Task<IEnumerable<TicketMapping>> GetByStatusAsync(string status)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.TicketMappings WHERE SyncStatus = @Status ORDER BY CreatedAt DESC";
        return await connection.QueryAsync<TicketMapping>(query, new { Status = status });
    }

    public async Task<int> AddAsync(TicketMapping ticketMapping)
    {
        using var connection = _context.CreateConnection();
        var query = @"INSERT INTO dbo.TicketMappings (JiraTicketKey, JiraTicketId, AseeTicketId, SyncStatus, CreatedAt, UpdatedAt, LastSyncedAt)
                      VALUES (@JiraTicketKey, @JiraTicketId, @AseeTicketId, @SyncStatus, @CreatedAt, @UpdatedAt, @LastSyncedAt);
                      SELECT CAST(SCOPE_IDENTITY() as int)";
        return await connection.ExecuteScalarAsync<int>(query, ticketMapping);
    }

    public async Task<int> UpdateAsync(TicketMapping ticketMapping)
    {
        using var connection = _context.CreateConnection();
        var query = @"UPDATE dbo.TicketMappings 
                      SET JiraTicketKey = @JiraTicketKey, JiraTicketId = @JiraTicketId, AseeTicketId = @AseeTicketId,
                          SyncStatus = @SyncStatus, UpdatedAt = @UpdatedAt, LastSyncedAt = @LastSyncedAt
                      WHERE Id = @Id";
        return await connection.ExecuteAsync(query, ticketMapping);
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var query = "DELETE FROM dbo.TicketMappings WHERE Id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}
