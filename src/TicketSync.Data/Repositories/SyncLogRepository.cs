using Dapper;
using TicketSync.Core.Interfaces;
using TicketSync.Core.Models;

namespace TicketSync.Data.Repositories;

public class SyncLogRepository : ISyncLogRepository
{
    private readonly DapperContext _context;

    public SyncLogRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<SyncLog?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.SyncLogs WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<SyncLog>(query, new { Id = id });
    }

    public async Task<IEnumerable<SyncLog>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.SyncLogs ORDER BY ExecutedAt DESC";
        return await connection.QueryAsync<SyncLog>(query);
    }

    public async Task<IEnumerable<SyncLog>> GetByTicketMappingIdAsync(int ticketMappingId)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.SyncLogs WHERE TicketMappingId = @TicketMappingId ORDER BY ExecutedAt DESC";
        return await connection.QueryAsync<SyncLog>(query, new { TicketMappingId = ticketMappingId });
    }

    public async Task<IEnumerable<SyncLog>> GetFailedLogsAsync()
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.SyncLogs WHERE Status = 'FAILED' ORDER BY ExecutedAt DESC";
        return await connection.QueryAsync<SyncLog>(query);
    }

    public async Task<IEnumerable<SyncLog>> GetLogsBySyncDirectionAsync(string syncDirection)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.SyncLogs WHERE SyncDirection = @SyncDirection ORDER BY ExecutedAt DESC";
        return await connection.QueryAsync<SyncLog>(query, new { SyncDirection = syncDirection });
    }

    public async Task<int> AddAsync(SyncLog syncLog)
    {
        using var connection = _context.CreateConnection();
        var query = @"INSERT INTO dbo.SyncLogs (TicketMappingId, SyncDirection, ActionType, SourceSystem, TargetSystem, Details, Status, ErrorMessage, ExecutedAt)
                      VALUES (@TicketMappingId, @SyncDirection, @ActionType, @SourceSystem, @TargetSystem, @Details, @Status, @ErrorMessage, @ExecutedAt);
                      SELECT CAST(SCOPE_IDENTITY() as int)";
        return await connection.ExecuteScalarAsync<int>(query, syncLog);
    }

    public async Task<int> UpdateAsync(SyncLog syncLog)
    {
        using var connection = _context.CreateConnection();
        var query = @"UPDATE dbo.SyncLogs 
                      SET TicketMappingId = @TicketMappingId, SyncDirection = @SyncDirection, ActionType = @ActionType,
                          SourceSystem = @SourceSystem, TargetSystem = @TargetSystem, Details = @Details, 
                          Status = @Status, ErrorMessage = @ErrorMessage, ExecutedAt = @ExecutedAt
                      WHERE Id = @Id";
        return await connection.ExecuteAsync(query, syncLog);
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var query = "DELETE FROM dbo.SyncLogs WHERE Id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}
