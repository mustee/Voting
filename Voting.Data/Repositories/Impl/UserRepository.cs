using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voting.Data.Models;
using Voting.Data.Models.Enums;

namespace Voting.Data.Repositories.Impl
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDataAccess dataAccess)
            : base(dataAccess){ }

        public override Task<long> Add(User entity)
        {
            return DataAccess.Execute<long>(DbScripts.DbScripts.User.Insert,
                new
                {
                    entity.AuthID,
                    entity.AuthType,
                    entity.ConfirmationToken,
                    entity.Confirmed,
                    entity.Country,
                    entity.DateConfirmed,
                    entity.DateJoined,
                    entity.Deleted,
                    entity.Email,
                    entity.FirstName,
                    entity.LastLogin,
                    entity.LastName,
                    entity.MiddleName,
                    entity.MobileNumber,
                    entity.MobileNumberCode,
                    entity.MobileNumberConfirmed,
                    entity.Password,
                    entity.Salt,
                    entity.ForgotPasswordTimeStamp,
                    entity.ForgotPasswordToken,
                    entity.Role
                });
        }

        public override Task<User> Find(long id)
        {
            return DataAccess.Find<User>(DbScripts.DbScripts.User.Select, new { Id = id });
        }

        public Task<IEnumerable<User>> FindAll()
        {
            return DataAccess.FindAll<User>(DbScripts.DbScripts.User.FindAll);
        }

        public Task<User> FindByAuthTypeAndAuthId(AuthType authType, string authId)
        {
            return DataAccess.Find<User>(DbScripts.DbScripts.User.FindByAuthTypeAndAuthId,
                new { AuthType = authType, AuthId = authId });
        }

        public Task<User> FindByConfirmationToken(string token)
        {
            return DataAccess.Find<User>(DbScripts.DbScripts.User.FindByConfirmationToken, new { ConfirmationToken = token });
        }

        public Task<User> FindByForgotPasswordToken(string token)
        {
            return DataAccess.Find<User>(DbScripts.DbScripts.User.FindByForgotPasswordToken, new { ForgotPasswordToken = token });
        }

        public Task<User> FindByEmail(string email)
        {
            return DataAccess.Find<User>(DbScripts.DbScripts.User.FindByEmail, new { Email = email });
        }

        public override Task<int> Remove(long id)
        {
            throw new NotImplementedException();
        }

        public override Task<int> Update(User entity)
        {
            return DataAccess.Execute<int>(DbScripts.DbScripts.User.Update,
                new
                {
                    entity.Id,
                    entity.AuthID,
                    entity.AuthType,
                    entity.ConfirmationToken,
                    entity.Confirmed,
                    entity.Country,
                    entity.DateConfirmed,
                    entity.DateJoined,
                    entity.Deleted,
                    entity.Email,
                    entity.FirstName,
                    entity.LastLogin,
                    entity.LastName,
                    entity.MiddleName,
                    entity.MobileNumber,
                    entity.MobileNumberCode,
                    entity.MobileNumberConfirmed,
                    entity.Password,
                    entity.Salt,
                    entity.ForgotPasswordTimeStamp,
                    entity.ForgotPasswordToken,
                    entity.Role
                });
        }
    }
}
