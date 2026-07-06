using Dapper;
using Npgsql;
using System.Data;
using TicketSystem.Entities;

namespace TicketSystem.Repositories
{
    public class SalesChannelRepository : ISalesChannelRepository
    {
        private readonly string? connectionString;

        public SalesChannelRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<SalesChannel>> GetAllSalesChannels()
        {
            const string query = "SELECT * FROM SalesChannels_GetAll_SortByCreatedAt();";

            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var SalesChannels = await conexion.QueryAsync<SalesChannel>(query);
                return SalesChannels;
            }
        }

        public async Task<SalesChannel?> GetSalesChannel(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", id, DbType.Int32, ParameterDirection.Input);

                string sql = "SELECT * FROM SalesChannel_GetById(@p_id);";
                var SalesChannel = await conexion.QueryFirstOrDefaultAsync<SalesChannel>(sql, parameters);
                return SalesChannel;
            }
        }

        public async Task<bool> ExistsSalesChannelById(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", id, DbType.Int32, ParameterDirection.Input);

                string sql = "SELECT * FROM SalesChannel_ExistsById(@p_id);";
                var exists = await conexion.QuerySingleAsync<bool>(sql, parameters);
                return exists;
            }
        }

        public async Task<int> CreateSalesChannel(SalesChannel SalesChannel)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_channelcode", SalesChannel.ChannelCode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_name", SalesChannel.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("p_type", SalesChannel.Type, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_apikey", SalesChannel.ApiKey, DbType.String, ParameterDirection.Input);
                parameters.Add("p_isactive", SalesChannel.IsActive, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("p_createdat", DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc), DbType.DateTimeOffset, ParameterDirection.Input);
                parameters.Add("nextid", dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                string sql = "CALL SalesChannel_Insert(@p_channelcode, @p_name, @p_type, @p_apikey, @p_isactive, @p_createdat::timestamp, @nextid);";
                var id = await conexion.QuerySingleAsync<int>(sql, parameters, commandType: CommandType.Text);
                SalesChannel.Id = parameters.Get<int>("nextid");
                return id;
            }
        }

        public async Task UpdateSalesChannel(SalesChannel SalesChannel)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_id", SalesChannel.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_channelcode", SalesChannel.ChannelCode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_name", SalesChannel.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("p_type", SalesChannel.Type, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_apikey", SalesChannel.ApiKey, DbType.String, ParameterDirection.Input);
                parameters.Add("p_isactive", SalesChannel.IsActive, DbType.Boolean, ParameterDirection.Input);

                string sql = "CALL SalesChannel_Update(@p_id, @p_channelcode, @p_name, @p_type, @p_apikey, @p_isactive);";
                await conexion.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
            }
        }

        public async Task DeleteSalesChannel(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_id", id, DbType.Int32, ParameterDirection.Input);
                string sql = "CALL SalesChannel_DeleteById(@p_id);";
                await conexion.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
            }
        }
    }
}
