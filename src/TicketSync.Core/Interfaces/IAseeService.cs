using TicketSync.Core.Models;

namespace TicketSync.Core.Interfaces;

public interface IAseeService
{
    Task<AseeTicket?> GetTicketAsync(string ticketId);
    Task<IEnumerable<AseeTicket>> GetTicketsAsync(int pageSize = 100, int pageNumber = 1);
    Task<AseeTicket> CreateTicketAsync(AseeTicket ticket);
    Task<AseeTicket> UpdateTicketAsync(string ticketId, AseeTicket ticket);
    Task<bool> CloseTicketAsync(string ticketId);
    Task<IEnumerable<object>> GetTicketChangesAsync(string ticketId, DateTime since);
}
