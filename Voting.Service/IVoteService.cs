using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Data.Models;

namespace Voting.Service
{
    public interface IVoteService
    {
        Task<IEnumerable<Position>> GetPositions();

        Task<IEnumerable<Candidate>> GetCandidatesByPosition(long positionId);

        Task<Vote> GetUserVote(long userId, long positionId);

        Task<Candidate> FindCandidate(long candidateId);

        Task<long> AddVote(long userId, long candidateId, DateTime timeStamp);
    }
}