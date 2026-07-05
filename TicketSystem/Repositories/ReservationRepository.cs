using Dapper;
using Npgsql;
using System.Data;
using TicketSystem.Entities;

namespace TicketSystem.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly string? connectionString;

        public ReservationRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            const string query = "SELECT * FROM Reservations_GetAll_SortByReservedAt();";

            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var Reservations = await conexion.QueryAsync<Reservation>(query);
                return Reservations;
            }
        }

        public async Task<Reservation?> GetReservation(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", id, DbType.Int32, ParameterDirection.Input);

                string sql = "SELECT * FROM Reservation_GetById(@p_id);";
                var Reservation = await conexion.QueryFirstOrDefaultAsync<Reservation>(sql, parameters);
                return Reservation;
            }
        }

        public async Task<bool> ExistsReservationById(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", id, DbType.Int32, ParameterDirection.Input);

                string sql = "SELECT * FROM Reservation_ExistsById(@p_id);";
                var exists = await conexion.QuerySingleAsync<bool>(sql, parameters);
                return exists;
            }
        }

        public async Task<int> CreateReservation(Reservation Reservation)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_reservationcode", Reservation.ReservationCode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_userid", Reservation.UserId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_eventid", Reservation.EventId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_saleschannelid", Reservation.SalesChannelId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_ticketquantity", Reservation.TicketQuantity, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_totalamount", Reservation.TotalAmount, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("p_status", Reservation.Status, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_reservedat", DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc), DbType.DateTimeOffset, ParameterDirection.Input);
                parameters.Add("nextid", dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                string sql = "CALL Reservation_Insert(@p_reservationcode, @p_userid, @p_eventid, @p_saleschannelid, @p_ticketquantity, @p_totalamount, @p_status, @p_reservedat::timestamp, @nextid);";
                var id = await conexion.QuerySingleAsync<int>(sql, parameters, commandType: CommandType.Text);
                Reservation.Id = parameters.Get<int>("nextid");
                return id;
            }
        }

        public async Task UpdateReservation(Reservation Reservation)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_id", Reservation.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_reservationcode", Reservation.ReservationCode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_userid", Reservation.UserId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_eventid", Reservation.EventId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_saleschannelid", Reservation.SalesChannelId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_ticketquantity", Reservation.TicketQuantity, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_totalamount", Reservation.TotalAmount, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("p_status", Reservation.Status, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_expiresat", DateTime.SpecifyKind((DateTime)Reservation.ExpiresAt, DateTimeKind.Utc), DbType.DateTimeOffset, ParameterDirection.Input);
                parameters.Add("p_confirmedat", DateTime.SpecifyKind((DateTime)Reservation.ConfirmedAt, DateTimeKind.Utc), DbType.DateTimeOffset, ParameterDirection.Input);

                string sql = "CALL Reservation_Update(@p_id, @p_reservationcode, @p_userid, @p_eventid, @p_saleschannelid, @p_ticketquantity, @p_totalamount, @p_status, @p_expiresat::timestamp, @p_confirmedat::timestamp);";
                await conexion.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
            }
        }

        public async Task DeleteReservation(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_id", id, DbType.Int32, ParameterDirection.Input);
                string sql = "CALL Reservation_DeleteById(@p_id);";
                await conexion.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
            }
        }
    }
}
