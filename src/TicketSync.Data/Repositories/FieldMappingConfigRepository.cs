using Dapper;
using TicketSync.Core.Interfaces;
using TicketSync.Core.Models;

namespace TicketSync.Data.Repositories;

public class FieldMappingConfigRepository : IFieldMappingConfigRepository
{
    private readonly DapperContext _context;

    public FieldMappingConfigRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<FieldMappingConfig?> GetByIdAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.FieldMappingConfig WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<FieldMappingConfig>(query, new { Id = id });
    }

    public async Task<IEnumerable<FieldMappingConfig>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.FieldMappingConfig ORDER BY CreatedAt DESC";
        return await connection.QueryAsync<FieldMappingConfig>(query);
    }

    public async Task<FieldMappingConfig?> GetByJiraAndAseeFieldAsync(string jiraField, string aseeField)
    {
        using var connection = _context.CreateConnection();
        var query = "SELECT * FROM dbo.FieldMappingConfig WHERE JiraFieldName = @JiraField AND AseeFieldName = @AseeField";
        return await connection.QueryFirstOrDefaultAsync<FieldMappingConfig>(query, new { JiraField = jiraField, AseeField = aseeField });
    }

    public async Task<int> AddAsync(FieldMappingConfig config)
    {
        using var connection = _context.CreateConnection();
        var query = @"INSERT INTO dbo.FieldMappingConfig (JiraFieldName, AseeFieldName, FieldType, IsMappingRequired, TransformationRule, CreatedAt)
                      VALUES (@JiraFieldName, @AseeFieldName, @FieldType, @IsMappingRequired, @TransformationRule, @CreatedAt);
                      SELECT CAST(SCOPE_IDENTITY() as int)";
        return await connection.ExecuteScalarAsync<int>(query, config);
    }

    public async Task<int> UpdateAsync(FieldMappingConfig config)
    {
        using var connection = _context.CreateConnection();
        var query = @"UPDATE dbo.FieldMappingConfig 
                      SET JiraFieldName = @JiraFieldName, AseeFieldName = @AseeFieldName, FieldType = @FieldType,
                          IsMappingRequired = @IsMappingRequired, TransformationRule = @TransformationRule
                      WHERE Id = @Id";
        return await connection.ExecuteAsync(query, config);
    }

    public async Task<int> DeleteAsync(int id)
    {
        using var connection = _context.CreateConnection();
        var query = "DELETE FROM dbo.FieldMappingConfig WHERE Id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}
