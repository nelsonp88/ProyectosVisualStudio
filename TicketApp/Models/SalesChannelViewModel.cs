using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketApp.Models
{
    public class SalesChannelViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Código")]
        public string ChannelCode { get; set; } = string.Empty;

        [Display(Name = "Nombre")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Tipo")]
        public ChannelType Type { get; set; }

        [Display(Name = "ApiKey")]
        public string ApiKey { get; set; } = string.Empty;

        [Display(Name = "Activo")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Fecha de creación")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public enum ChannelType
    {
        WebPortal,
        Office,
        CollaboratorWeb,
        CollaboratorPOS,
        MobileApp
    }
}
