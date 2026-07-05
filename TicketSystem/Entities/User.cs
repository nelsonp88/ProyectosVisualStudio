using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystem.Entities;

public class User
{
    public int Id { get; set; }

    public string UserCode { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsActive { get; set; } = true;

    // Navigation
    //public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
