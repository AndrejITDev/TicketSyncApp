using Dapper;
using TicketSync.Core.Interfaces;
using TicketSync.Core.Models;

namespace TicketSync.Data.Repositories;

public class SyncRetryRepository : ISyncRetryRepository
{
    private readonly DapperContext _context;

    public SyncRetryRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<SyncRetry?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.SyncRetries WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<SyncRetry>(query, new { Id = id });
    }

    public async Task<IEnumerable<SyncRetry>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.SyncRetries ORDER BY NextRetryAt DESC";
        return await connection.QueryAsync<SyncRetry>(query);
    }

    public async Task<IEnumerable<SyncRetry>> GetByTicketMappingIdAsync(int ticketMappingId)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.SyncRetries WHERE TicketMappingId = @TicketMappingId";
        return await connection.QueryAsync<SyncRetry>(query, new { TicketMappingId = ticketMappingId });
    }

    public async Task<IEnumerable<SyncRetry>> GetPendingRetriesAsync()
    {
        using var connection = _context.CreateConnection();
        var query = @"SELECT * FROM dbo.SyncRetries 
                      WHERE Status IN ('PENDING', 'RETRYING') AND NextRetryAt <= GETUTCDATE()
                      ORDER BY NextRetryAt ASC";
        return await connection.QueryAsync<SyncRetry>(query);
    }

    public async Task<int> AddAsync(SyncRetry syncRetry)
    {
        using var connection = _context.CreateConnection();
        var query = @"INSERT INTO dbo.SyncRetries (TicketMappingId, SyncDirection, RetryCount, MaxRetries, LastRetryAt, NextRetryAt, Status, ErrorMessage)
                      VALUES (@TicketMappingId, @SyncDirection, @RetryCount, @MaxRetries, @LastRetryAt, @NextRetryAt, @Status, @ErrorMessage);
                      SELECT CAST(SCOPE_IDENTITY() as int)";
        return await connection.ExecuteScalarAsync<int>(query, syncRetry);
    }

    public async Task<int> UpdateAsync(SyncRetry syncRetry)
    {
        using var connection = _context.CreateConnection();
        var query = @"UPDATE dbo.SyncRetries 
                      SET TicketMappingId = @TicketMappingId, SyncDirection = @SyncDirection, RetryCount = @RetryCount,
                          MaxRetries = @MaxRetries, LastRetryAt = @LastRetryAt, NextRetryAt = @NextRetryAt,
                          Status = @Status, ErrorMessage = @ErrorMessage
                      WHERE Id = @Id";
        return await connection.ExecuteAsync(query, syncRetry);
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var query = "DELETE FROM dbo.SyncRetries WHERE Id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}
