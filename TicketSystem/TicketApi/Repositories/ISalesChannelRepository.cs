using TicketSystem.Entities;

namespace TicketSystem.Repositories
{
    public interface ISalesChannelRepository
    {
        Task<int> CreateSalesChannel(SalesChannel SalesChannel);
        Task DeleteSalesChannel(int id);
        Task<bool> ExistsSalesChannelById(int id);
        Task<IEnumerable<SalesChannel>> GetAllSalesChannels();
        Task<SalesChannel?> GetSalesChannel(int id);
        Task UpdateSalesChannel(SalesChannel SalesChannel);
    }
}