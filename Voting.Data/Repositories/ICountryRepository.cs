using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Data.Models;

namespace Voting.Data.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<Country> FindByName(string name);

        Task<IEnumerable<Country>> FindAll();
    }
}
