using TicketSystem.Entities;

namespace UserSystem.Repositories
{
    public interface IUserRepository
    {
        Task<int> CreateUser(User User);
        Task DeleteUser(int id);
        Task<bool> ExistsUserById(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetUser(int id);
        Task UpdateUser(User User);
    }
}