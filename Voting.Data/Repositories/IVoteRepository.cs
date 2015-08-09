using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Data.Models;

namespace Voting.Data.Repositories
{
    public interface IVoteRepository : IRepository<Vote>
    {
        Task<Vote> GetUserVote(long userId, long positionId);
    }
}
