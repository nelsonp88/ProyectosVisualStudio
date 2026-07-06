using Dapper;
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
            const string query = "SELECT * FROM Tickets_GetAll_SortByCreatedAt();";

            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var tickets = await conexion.QueryAsync<Ticket>(query);
                return tickets;
            }
        }

        public async Task<Ticket?> GetTicket(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", id, DbType.Int32, ParameterDirection.Input);

                string sql = "SELECT * FROM Ticket_GetById(@p_id);";
                var ticket = await conexion.QueryFirstOrDefaultAsync<Ticket>(sql, parameters);
                return ticket;
            }
        }

        public async Task<bool> ExistsTicketById(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", id, DbType.Int32, ParameterDirection.Input);

                string sql = "SELECT * FROM Ticket_ExistsById(@p_id);";
                var exists = await conexion.QuerySingleAsync<bool>(sql, parameters);
                return exists;
            }
        }

        public async Task<int> CreateTicket(Ticket ticket)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_ticketcode", ticket.TicketCode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_eventid", ticket.EventId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_reservationid", ticket.ReservationId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_status", ticket.Status, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_price", ticket.Price, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("p_seatnumber", ticket.SeatNumber, DbType.String, ParameterDirection.Input);
                parameters.Add("p_createdat", DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc), DbType.DateTimeOffset, ParameterDirection.Input);
                parameters.Add("nextid", dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                string sql = "CALL Ticket_Insert(@p_ticketcode, @p_eventid, @p_reservationid, @p_status, @p_price, @p_seatnumber, @p_createdat::timestamp, @nextid);";
                var id = await conexion.QuerySingleAsync<int>(sql, parameters, commandType: CommandType.Text);
                ticket.Id = parameters.Get<int>("nextid");
                return id;
            }
        }

        public async Task UpdateTicket(Ticket ticket)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_id", ticket.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_ticketcode", ticket.TicketCode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_eventid", ticket.EventId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_reservationid", ticket.ReservationId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_status", ticket.Status, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_price", ticket.Price, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("p_seatnumber", ticket.SeatNumber, DbType.String, ParameterDirection.Input);
                
                string sql = "CALL Ticket_Update(@p_id, @p_ticketcode, @p_eventid, @p_reservationid, @p_status, @p_price, @p_seatnumber);";
                await conexion.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
            }
        }

        public async Task DeleteTicket(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_id", id, DbType.Int32, ParameterDirection.Input);
                string sql = "CALL Ticket_DeleteById(@p_id);";
                await conexion.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
            }
        }
    }
}
