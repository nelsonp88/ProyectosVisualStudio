using Dapper;
using Npgsql;
using System.Data;
using TicketSystem.Entities;

namespace TicketSystem.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly string? connectionString;

        public EventRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            const string query = "SELECT * FROM Events_GetAll_SortByCreatedAt();";

            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var Events = await conexion.QueryAsync<Event>(query);
                return Events;
            }
        }

        public async Task<Event?> GetEvent(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", id, DbType.Int32, ParameterDirection.Input);

                string sql = "SELECT * FROM Event_GetById(@p_id);";
                var Event = await conexion.QueryFirstOrDefaultAsync<Event>(sql, parameters);
                return Event;
            }
        }

        public async Task<bool> ExistsEventById(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", id, DbType.Int32, ParameterDirection.Input);

                string sql = "SELECT * FROM Event_ExistsById(@p_id);";
                var exists = await conexion.QuerySingleAsync<bool>(sql, parameters);
                return exists;
            }
        }

        public async Task<int> CreateEvent(Event Event)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_eventcode", Event.EventCode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_name", Event.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("p_description", Event.Description, DbType.String, ParameterDirection.Input);
                parameters.Add("p_eventdate", DateTime.SpecifyKind(Event.EventDate, DateTimeKind.Utc), DbType.DateTimeOffset, ParameterDirection.Input);
                parameters.Add("p_venue", Event.Venue, DbType.String, ParameterDirection.Input);
                parameters.Add("p_totaltickets", Event.TotalTickets, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_availabletickets", Event.AvailableTickets, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_baseprice", Event.BasePrice, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("p_createdat", DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc), DbType.DateTimeOffset, ParameterDirection.Input);
                parameters.Add("p_isactive", Event.IsActive, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("nextid", dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                string sql = "CALL Event_Insert(@p_eventcode, @p_name, @p_description, @p_eventdate::timestamp, @p_venue, @p_totaltickets, @p_availabletickets, @p_baseprice, @p_createdat::timestamp, @p_isactive, @nextid);";
                var id = await conexion.QuerySingleAsync<int>(sql, parameters, commandType: CommandType.Text);
                Event.Id = parameters.Get<int>("nextid");
                return id;
            }
        }

        public async Task UpdateEvent(Event Event)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_id", Event.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_eventcode", Event.EventCode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_name", Event.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("p_description", Event.Description, DbType.String, ParameterDirection.Input);
                parameters.Add("p_eventdate", DateTime.SpecifyKind(Event.EventDate, DateTimeKind.Utc), DbType.DateTimeOffset, ParameterDirection.Input);
                parameters.Add("p_venue", Event.Venue, DbType.String, ParameterDirection.Input);
                parameters.Add("p_totaltickets", Event.TotalTickets, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_availabletickets", Event.AvailableTickets, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_baseprice", Event.BasePrice, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("p_isactive", Event.IsActive, DbType.Boolean, ParameterDirection.Input);

                string sql = "CALL Event_Update(@p_id, @p_eventcode, @p_name, @p_description, @p_eventdate::timestamp, @p_venue, @p_totaltickets, @p_availabletickets, @p_baseprice, @p_isactive);";
                await conexion.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
            }
        }

        public async Task DeleteEvent(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_id", id, DbType.Int32, ParameterDirection.Input);
                string sql = "CALL Event_DeleteById(@p_id);";
                await conexion.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
            }
        }
    }
}
