using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Data.Models;

namespace Voting.Data.Repositories.Impl
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(IDataAccess dataAccess)
            : base(dataAccess){ }

        public override Task<long> Add(Country entity)
        {
            throw new NotImplementedException();
        }

        public override Task<Country> Find(long id)
        {
            return DataAccess.Find<Country>(DbScripts.DbScripts.Country.Select, new { Id = id });
        }

        public Task<IEnumerable<Country>> FindAll()
        {
            return DataAccess.FindAll<Country>(DbScripts.DbScripts.Country.FindAll);
        }

        public Task<Country> FindByName(string name)
        {
            return DataAccess.Find<Country>(DbScripts.DbScripts.Country.FindByName, new { Name = name });
        }

        public override Task<int> Remove(long id)
        {
            throw new NotImplementedException();
        }

        public override Task<int> Update(Country entity)
        {
            throw new NotImplementedException();
        }
    }
}
