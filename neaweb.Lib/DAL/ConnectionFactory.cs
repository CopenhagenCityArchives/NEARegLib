using MySqlConnector;
using System.Data;

namespace neaweb_dapper.DAL
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string _connectionString;

        public IDbConnection GetConnection { get { return new MySqlConnection(_connectionString); } }

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
