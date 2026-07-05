using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Código")]
        public string EventCode { get; set; } = string.Empty;

        [Display(Name = "Nombre")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Descripción")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Fecha")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime EventDate { get; set; }

        [Display(Name = "Sede")]
        public string Venue { get; set; } = string.Empty;

        [Display(Name = "Total de entradas")]
        public int TotalTickets { get; set; }

        [Display(Name = "Entradas disponibles")]
        public int AvailableTickets { get; set; }

        [Display(Name = "Precio base")]
        public decimal BasePrice { get; set; }

        [Display(Name = "Fecha de creación")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Activo")]
        public bool IsActive { get; set; } = true;

        // Navigation
        //public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
