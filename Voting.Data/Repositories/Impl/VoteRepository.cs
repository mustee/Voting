using System;
using System.Threading.Tasks;
using Voting.Data.Models;

namespace Voting.Data.Repositories.Impl
{
    public class VoteRepository:RepositoryBase<Vote>, IVoteRepository
    {
        public VoteRepository(IDataAccess dataAccess) : base(dataAccess)
        {
        }

        public override Task<long> Add(Vote entity)
        {
            return DataAccess.Execute<long>(DbScripts.DbScripts.Vote.Insert,
                new {entity.UserId, entity.CandidateId, entity.TimeStamp});
        }

        public override Task<int> Remove(long id)
        {
            throw new NotImplementedException();
        }

        public override Task<int> Update(Vote entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Vote> Find(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Vote> GetUserVote(long userId, long positionId)
        {
            return DataAccess.Find<Vote>(DbScripts.DbScripts.Vote.GetUserVote,
                new {UserId = userId, PositionId = positionId });
        }
    }
}
