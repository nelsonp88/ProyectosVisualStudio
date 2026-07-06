using TicketSystem.Entities;

namespace TicketSystem.Repositories
{
    public interface IReservationRepository
    {
        Task<int> CreateReservation(Reservation Reservation);
        Task DeleteReservation(int id);
        Task<bool> ExistsReservationById(int id);
        Task<IEnumerable<Reservation>> GetAllReservations();
        Task<Reservation?> GetReservation(int id);
        Task UpdateReservation(Reservation Reservation);
    }
}