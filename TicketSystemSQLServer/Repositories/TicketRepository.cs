using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Data;
using TicketSystem.Entities;

namespace TicketSystem.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly string? connectionString;

        public TicketRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Ticket>> GetAllTickets()
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var tickets = await conexion.QueryAsync<Ticket>("Tickets_GetAll", commandType: CommandType.StoredProcedure);
                return tickets;
            }
        }

        public async Task<Ticket?> GetTicket(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var ticket = await conexion.QueryFirstOrDefaultAsync<Ticket>("Ticket_GetById", 
                    new {id},
                    commandType: CommandType.StoredProcedure);
                return ticket;
            }
        }

        public async Task<bool> ExistsTicketById(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var exists = await conexion.QuerySingleAsync<bool>("Ticket_ExistsById",
                    new { id },
                    commandType: CommandType.StoredProcedure);
                return exists;
            }
        }

        public async Task<int> CreateTicket(Ticket ticket)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var id = await conexion.QuerySingleAsync<int>("Ticket_Insert", 
                    new { ticket.TicketCode, ticket.EventId, ticket.ReservationId, ticket.Status, ticket.Price, ticket.SeatNumber, ticket.CreatedAt },
                    commandType: CommandType.StoredProcedure);
                ticket.Id = id;
                return id;
            }
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                await conexion.ExecuteAsync("Ticket_Update",
                    new { ticket.Id, ticket.TicketCode, ticket.EventId, ticket.ReservationId, ticket.Status, ticket.Price, ticket.SeatNumber },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteTicket(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                await conexion.ExecuteAsync("Ticket_DeleteById",
                    new { id },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
