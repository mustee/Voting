using System.Threading.Tasks;

namespace Voting.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : IEntity
    {
        protected readonly IDataAccess DataAccess;

        protected RepositoryBase(IDataAccess dataAccess)
        {
            DataAccess = dataAccess;
        }

        public abstract Task<long> Add(T entity);

        public abstract Task<T> Find(long id);

        public abstract Task<int> Remove(long id);

        public abstract Task<int> Update(T entity);
    }
}
