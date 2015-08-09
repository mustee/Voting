using Voting.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Data.Models.Enums;

namespace Voting.Data.Repositories
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> FindByEmail(string email);

        Task<User> FindByConfirmationToken(string token);

        Task<User> FindByForgotPasswordToken(string token);

        Task<IEnumerable<User>> FindAll();

        Task<User> FindByAuthTypeAndAuthId(AuthType authType, string authId);


    }
}
