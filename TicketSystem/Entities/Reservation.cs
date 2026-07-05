using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystem.Entities;

public class Reservation
{
    public int Id { get; set; }

    public string ReservationCode { get; set; } = string.Empty;

    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    //public User? User { get; set; }

    public int EventId { get; set; }

    public string EventName { get; set; } = string.Empty;

    //public Event? Event { get; set; }

    public int SalesChannelId { get; set; }

    public string SalesChannelName { get; set; } = string.Empty;

    //public SalesChannel? SalesChannel { get; set; }

    public int TicketQuantity { get; set; }

    public decimal TotalAmount { get; set; }

    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

    [Column(TypeName = "timestamp without time zone")] 
    public DateTime ReservedAt { get; set; } = DateTime.UtcNow;


    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ExpiresAt { get; set; }


    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ConfirmedAt { get; set; }

    // Navigation
    //public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

public enum ReservationStatus
{
    Pending,
    Confirmed,
    Expired,
    Cancelled
}
