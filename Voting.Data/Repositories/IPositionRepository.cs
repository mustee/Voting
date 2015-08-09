using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Data.Models;

namespace Voting.Data.Repositories
{
    public interface IPositionRepository : IRepository<Position>
    {
        Task<IEnumerable<Position>> FindAll();
    }
}
