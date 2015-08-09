using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.Data.Models;

namespace Voting.Data.Repositories.Impl
{
    public class PositionRepository:RepositoryBase<Position>, IPositionRepository
    {
        public PositionRepository(IDataAccess dataAccess) : base(dataAccess)
        {
        }

        public override Task<long> Add(Position entity)
        {
            throw new NotImplementedException();
        }

        public override Task<int> Remove(long id)
        {
            throw new NotImplementedException();
        }

        public override Task<int> Update(Position entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Position> Find(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Position>> FindAll()
        {
            return DataAccess.FindAll<Position>(DbScripts.DbScripts.Position.FindAll);
        }
    }
}
