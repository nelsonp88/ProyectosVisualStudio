using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    public class ReservationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Código")]
        public string ReservationCode { get; set; } = string.Empty;

        public int UserId { get; set; }

        [Display(Name = "Usuario")]
        public string UserName { get; set; } = string.Empty;

        //public User? User { get; set; }

        public IEnumerable<SelectListItem> UserItems { get; set; }

        public int EventId { get; set; }

        [Display(Name = "Evento")]
        public string EventName { get; set; } = string.Empty;

        //public Event? Event { get; set; }

        public IEnumerable<SelectListItem> EventItems { get; set; }

        public int SalesChannelId { get; set; }

        //public SalesChannel? SalesChannel { get; set; }

        public IEnumerable<SelectListItem> SalesChannelItems { get; set; }

        [Display(Name = "Canal de ventas")]
        public string SalesChannelName { get; set; } = string.Empty;

        [Display(Name = "Cantidad de entradas")]
        public int TicketQuantity { get; set; }

        [Display(Name = "Total")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Estado")]
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        [Display(Name = "Fecha de reserva")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime ReservedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Fecha de vencimiento")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime? ExpiresAt { get; set; }

        [Display(Name = "Fecha de confirmación")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime? ConfirmedAt { get; set; }

        // Navigation
        //public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }

    public enum ReservationStatus
    {
        [Display(Name = "Pendiente")]
        Pending,
        [Display(Name = "Confirmada")]
        Confirmed,
        [Display(Name = "Vencida")]
        Expired,
        [Display(Name = "Cancelada")]
        Cancelled
    }
}
