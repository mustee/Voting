using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Data.Models;
using Voting.Data.Models.Enums;

namespace Voting.Service
{
    public interface IUserService
    {
        Task<long> RegisterUser(string email, string password, string lastName, string firstName, 
            string middleName, AuthType? authType, string authId, bool confirmEmail);

        Task SendConfirmationEmail(User user);

        Task<User> FindByEmail(string email);

        Task<User> FindByAuthTypeAndAuthId(AuthType authType, string authId);

        Task<User> FindByConfirmationToken(string token);

        Task<User> FindByForgotPasswordToken(string token);

        Task<int> UpdateUser(User user);

        Task<Country> FindCountry(long id);

        Task<IEnumerable<Country>> GetAllCountries();

        Task<bool> UpdatePhoneNumber(long userId, long country, string phoneNumber);

        Task<bool> ConfirmPhoneNumberCode(long userId, string code);

        Task<bool> ForgotPassword(long userId);

        Task<bool> SetPassword(long userId, string password);
    }
}
