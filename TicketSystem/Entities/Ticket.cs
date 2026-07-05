using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystem.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        public string TicketCode { get; set; } = string.Empty;

        public int EventId { get; set; }
        
        //public Event? Event { get; set; }

        public int? ReservationId { get; set; }
        
        //public Reservation? Reservation { get; set; }

        public TicketStatus Status { get; set; } = TicketStatus.Available;

        public decimal Price { get; set; }

        public string SeatNumber { get; set; } = string.Empty;


        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum TicketStatus
    {
        Available,
        Reserved,
        Sold,
        Cancelled
    }
}
