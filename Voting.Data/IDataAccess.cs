using System.Collections.Generic;
using System.Threading.Tasks;

namespace Voting.Data
{
    public interface IDataAccess
    {
        Task<T> Execute<T>(string sql, dynamic param = null);

        Task<T> Find<T>(string sql, dynamic param = null, bool buffered = true);

        Task<IEnumerable<T>> FindAll<T>(string sql, dynamic param = null, bool buffered = true);
    }
}
