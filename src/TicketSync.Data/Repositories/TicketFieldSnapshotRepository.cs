using Dapper;
using TicketSync.Core.Interfaces;
using TicketSync.Core.Models;

namespace TicketSync.Data.Repositories;

public class TicketFieldSnapshotRepository : ITicketFieldSnapshotRepository
{
    private readonly DapperContext _context;

    public TicketFieldSnapshotRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<TicketFieldSnapshot?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.TicketFieldSnapshots WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<TicketFieldSnapshot>(query, new { Id = id });
    }

    public async Task<IEnumerable<TicketFieldSnapshot>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.TicketFieldSnapshots ORDER BY ChangedAt DESC";
        return await connection.QueryAsync<TicketFieldSnapshot>(query);
    }

    public async Task<IEnumerable<TicketFieldSnapshot>> GetByTicketMappingIdAsync(int ticketMappingId)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.TicketFieldSnapshots WHERE TicketMappingId = @TicketMappingId ORDER BY ChangedAt DESC";
        return await connection.QueryAsync<TicketFieldSnapshot>(query, new { TicketMappingId = ticketMappingId });
    }

    public async Task<int> AddAsync(TicketFieldSnapshot snapshot)
    {
        using var connection = _context.CreateConnection();
        var query = @"INSERT INTO dbo.TicketFieldSnapshots (TicketMappingId, SnapshotSystem, FieldName, OldValue, NewValue, ChangedAt)
                      VALUES (@TicketMappingId, @SnapshotSystem, @FieldName, @OldValue, @NewValue, @ChangedAt);
                      SELECT CAST(SCOPE_IDENTITY() as int)";
        return await connection.ExecuteScalarAsync<int>(query, snapshot);
    }

    public async Task<int> UpdateAsync(TicketFieldSnapshot snapshot)
    {
        using var connection = _context.CreateConnection();
        var query = @"UPDATE dbo.TicketFieldSnapshots 
                      SET TicketMappingId = @TicketMappingId, SnapshotSystem = @SnapshotSystem, FieldName = @FieldName,
                          OldValue = @OldValue, NewValue = @NewValue, ChangedAt = @ChangedAt
                      WHERE Id = @Id";
        return await connection.ExecuteAsync(query, snapshot);
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var query = "DELETE FROM dbo.TicketFieldSnapshots WHERE Id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}
