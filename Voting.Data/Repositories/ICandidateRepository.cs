using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Data.Models;

namespace Voting.Data.Repositories
{
    public interface ICandidateRepository:IRepository<Candidate>
    {
        Task<IEnumerable<Candidate>> FindByPosition(long positionId);
    }
}
