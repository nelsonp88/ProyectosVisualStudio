using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystem.Entities;

public class Event
{
    public int Id { get; set; }

    public string EventCode { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;


    [Column(TypeName = "timestamp without time zone")]
    public DateTime EventDate { get; set; }

    public string Venue { get; set; } = string.Empty;

    public int TotalTickets { get; set; }

    public int AvailableTickets { get; set; }

    public decimal BasePrice { get; set; }


    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    // Navigation
    //public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
