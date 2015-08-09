using System.Threading.Tasks;

namespace Voting.Data.Repositories
{
    public interface IRepository<T>
        where T: IEntity
    {
        Task<long> Add(T entity);

        Task<int> Remove(long id);

        Task<int> Update(T entity);

        Task<T> Find(long id);
    }
}
