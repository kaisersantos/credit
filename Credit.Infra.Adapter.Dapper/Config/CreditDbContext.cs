using Microsoft.Data.SqlClient;
using System.Data;

namespace Credit.Infra.Adapter.Dapper.Config
{
    public class CreditDbContext
    {
        private readonly string _connectionString;

        public CreditDbContext(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
