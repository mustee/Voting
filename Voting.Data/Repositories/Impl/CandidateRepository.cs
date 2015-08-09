using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Data.Models;

namespace Voting.Data.Repositories.Impl
{
    public class CandidateRepository:RepositoryBase<Candidate>, ICandidateRepository
    {
        public CandidateRepository(IDataAccess dataAccess) : base(dataAccess)
        {
        }

        public override Task<long> Add(Candidate entity)
        {
            throw new NotImplementedException();
        }

        public override Task<int> Remove(long id)
        {
            throw new NotImplementedException();
        }

        public override Task<int> Update(Candidate entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Candidate> Find(long id)
        {
            return DataAccess.Find<Candidate>(DbScripts.DbScripts.Candidate.Select, new {Id = id});
        }

        public Task<IEnumerable<Candidate>> FindByPosition(long positionId)
        {
            return DataAccess.FindAll<Candidate>(DbScripts.DbScripts.Candidate.FindByPosition,
                new {PositionId = positionId});
        }
    }
}
