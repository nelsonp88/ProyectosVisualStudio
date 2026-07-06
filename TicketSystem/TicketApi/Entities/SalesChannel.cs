using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystem.Entities
{
    public class SalesChannel
    {
        public int Id { get; set; }

        public string ChannelCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public ChannelType Type { get; set; }

        public string ApiKey { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;


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
