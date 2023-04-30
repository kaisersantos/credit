using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Credit.Infra.Adapter.Dapper.Config
{
    public class CreditDbContext
    {
        private IDbConnection _connection;

        public CreditDbContext(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            _connection = new SqlConnection(connectionString);
        }

        private void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }

        private void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        public async Task<int> ExecuteAsync(string command, object? param = null)
        {
            OpenConnection();

            var affectedRows = await _connection.ExecuteAsync(command, param);

            CloseConnection();

            return affectedRows;
        }

        public async Task<T> QuerySingleAsync<T>(string command, object? param = null)
        {
            OpenConnection();

            var result = await _connection.QuerySingleAsync<T>(command, param);

            CloseConnection();

            return result;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object? param = null)
        {
            OpenConnection();

            var result = await _connection.QueryAsync<T>(query, param);

            CloseConnection();

            return result;
        }

        public async Task<T> QueryFirstOrDefault<T>(string query, object? param = null)
        {
            OpenConnection();

            var result = await _connection.QueryFirstOrDefaultAsync<T>(query, param);

            CloseConnection();

            return result;
        }
    }
}
