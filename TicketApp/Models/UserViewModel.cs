using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Código")]
        public string UserCode { get; set; } = string.Empty;

        [Display(Name = "Nombre")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "E-mail")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Teléfono")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Fecha de creación")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Activo")]
        public bool IsActive { get; set; } = true;

        // Navigation
        //public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
