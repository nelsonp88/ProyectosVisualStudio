using TicketSystem.Entities;

namespace TicketSystem.Repositories
{
    public interface ITicketRepository
    {
        Task<int> CreateTicket(Ticket ticket);
        Task DeleteTicket(int id);
        Task<bool> ExistsTicketById(int id);
        Task<IEnumerable<Ticket>> GetAllTickets();
        Task<Ticket?> GetTicket(int id);
        Task UpdateTicket(Ticket ticket);
    }
}