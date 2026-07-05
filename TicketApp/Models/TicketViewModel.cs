using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Código")]
        public string TicketCode { get; set; } = string.Empty;

        [Display(Name = "Evento")]
        public int EventId { get; set; }

        //public Event? Event { get; set; }

        [Display(Name = "Código de reserva")]
        public int? ReservationId { get; set; }

        //public Reservation? Reservation { get; set; }

        [Display(Name = "Estado")]
        public TicketStatus Status { get; set; } = TicketStatus.Available;

        [Display(Name = "Precio")]
        public decimal Price { get; set; }

        [Display(Name = "No. Silla")]
        public string SeatNumber { get; set; } = string.Empty;

        [Display(Name = "Fecha de creación")]

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
