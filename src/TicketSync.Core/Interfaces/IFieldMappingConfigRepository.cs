using TicketSync.Core.Models;

namespace TicketSync.Core.Interfaces;

public interface IFieldMappingConfigRepository
{
    Task<FieldMappingConfig?> GetByIdAsync(int id);
    Task<IEnumerable<FieldMappingConfig>> GetAllAsync();
    Task<FieldMappingConfig?> GetByJiraAndAseeFieldAsync(string jiraField, string aseeField);
    Task<int> AddAsync(FieldMappingConfig config);
    Task<int> UpdateAsync(FieldMappingConfig config);
    Task<int> DeleteAsync(int id);
}
