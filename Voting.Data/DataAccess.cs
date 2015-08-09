using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Threading.Tasks;

namespace Voting.Data
{
    public class DataAccess : IDataAccess
    {
        private readonly IDatabaseConnection databaseConnection;
        public DataAccess(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public Task<T> Execute<T>(string sql, dynamic param = null)
        {
            Task<T> result;
            using (var connection = databaseConnection.Connection)
            {
                result = SqlMapper.ExecuteScalarAsync<T>(databaseConnection.Connection, sql, param);
            }

            return result;
        }

        public Task<T> Find<T>(string sql, dynamic param = null, bool buffered = true)
        {
            Task<T> result;
            using (var connection = databaseConnection.Connection)
            {
                var items = (Task<IEnumerable<T>>)SqlMapper.QueryAsync<T>(databaseConnection.Connection, sql, param);
                result = Task.FromResult(items.Result.FirstOrDefault());
            }

            return result;
        }

        public Task<IEnumerable<T>> FindAll<T>(string sql, dynamic param = null, bool buffered = true)
        {
            Task <IEnumerable <T>> result;
            using (var connection = databaseConnection.Connection)
            {
                result = SqlMapper.QueryAsync<T>(databaseConnection.Connection, sql, param);
            }

            return result;
        }
    }
}
