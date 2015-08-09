using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Data.Models;
using Voting.Data.Repositories;

namespace Voting.Service.Impl
{
    public class VoteService:IVoteService
    {
        private readonly IPositionRepository positionRepository;
        private readonly ICandidateRepository candidateRepository;
        private readonly IVoteRepository voteRepository;

        public VoteService(IPositionRepository positionRepository, ICandidateRepository candidateRepository, IVoteRepository voteRepository)
        {
            this.positionRepository = positionRepository;
            this.candidateRepository = candidateRepository;
            this.voteRepository = voteRepository;
        }

        public Task<IEnumerable<Position>> GetPositions()
        {
            return positionRepository.FindAll();
        }

        public Task<IEnumerable<Candidate>> GetCandidatesByPosition(long positionId)
        {
            return candidateRepository.FindByPosition(positionId);
        }

        public Task<Vote> GetUserVote(long userId, long positionId)
        {
            return voteRepository.GetUserVote(userId, positionId);
        }

        public Task<Candidate> FindCandidate(long candidateId)
        {
            return candidateRepository.Find(candidateId);
        }

        public Task<long> AddVote(long userId, long candidateId, DateTime timeStamp)
        {
            return voteRepository.Add(new Vote {UserId = userId, CandidateId = candidateId, TimeStamp = timeStamp});
        }
    }
}
