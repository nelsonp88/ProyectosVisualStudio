using Dapper;
using Npgsql;
using System.Data;
using TicketSystem.Entities;

namespace UserSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string? connectionString;

        public UserRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            const string query = "SELECT * FROM Users_GetAll_SortByCreatedAt();";

            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var Users = await conexion.QueryAsync<User>(query);
                return Users;
            }
        }

        public async Task<User?> GetUser(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", id, DbType.Int32, ParameterDirection.Input);

                string sql = "SELECT * FROM User_GetById(@p_id);";
                var User = await conexion.QueryFirstOrDefaultAsync<User>(sql, parameters);
                return User;
            }
        }

        public async Task<bool> ExistsUserById(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("p_id", id, DbType.Int32, ParameterDirection.Input);

                string sql = "SELECT * FROM User_ExistsById(@p_id);";
                var exists = await conexion.QuerySingleAsync<bool>(sql, parameters);
                return exists;
            }
        }

        public async Task<int> CreateUser(User User)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_usercode", User.UserCode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_name", User.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("p_email", User.Email, DbType.String, ParameterDirection.Input);
                parameters.Add("p_phonenumber", User.PhoneNumber, DbType.String, ParameterDirection.Input);
                parameters.Add("p_createdat", DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc), DbType.DateTimeOffset, ParameterDirection.Input);
                parameters.Add("p_isactive", User.IsActive, DbType.Boolean, ParameterDirection.Input);
                parameters.Add("nextid", dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                string sql = "CALL User_Insert(@p_usercode, @p_name, @p_email, @p_phonenumber, @p_createdat::timestamp, @p_isactive, @nextid);";
                var id = await conexion.QuerySingleAsync<int>(sql, parameters, commandType: CommandType.Text);
                User.Id = parameters.Get<int>("nextid");
                return id;
            }
        }

        public async Task UpdateUser(User User)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_id", User.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("p_usercode", User.UserCode, DbType.String, ParameterDirection.Input);
                parameters.Add("p_name", User.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("p_email", User.Email, DbType.String, ParameterDirection.Input);
                parameters.Add("p_phonenumber", User.PhoneNumber, DbType.String, ParameterDirection.Input);
                parameters.Add("p_isactive", User.IsActive, DbType.Boolean, ParameterDirection.Input);

                string sql = "CALL User_Update(@p_id, @p_usercode, @p_name, @p_email, @p_phonenumber, @p_isactive);";
                await conexion.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
            }
        }

        public async Task DeleteUser(int id)
        {
            using (var conexion = new NpgsqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();

                parameters.Add("p_id", id, DbType.Int32, ParameterDirection.Input);
                string sql = "CALL User_DeleteById(@p_id);";
                await conexion.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
            }
        }
    }
}
