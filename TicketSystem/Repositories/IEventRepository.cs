using TicketSystem.Entities;

namespace TicketSystem.Repositories
{
    public interface IEventRepository
    {
        Task<int> CreateEvent(Event Event);
        Task DeleteEvent(int id);
        Task<bool> ExistsEventById(int id);
        Task<IEnumerable<Event>> GetAllEvents();
        Task<Event?> GetEvent(int id);
        Task UpdateEvent(Event Event);
    }
}