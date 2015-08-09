using System.Data.Common;
using System.Data.SqlClient;

namespace Voting.Data
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly string connectionString;
        public DatabaseConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbConnection Connection => new SqlConnection(connectionString);
    }
}
