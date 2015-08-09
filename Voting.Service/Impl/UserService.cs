using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using Voting.Common;
using Voting.Data.Models;
using Voting.Data.Models.Enums;
using Voting.Data.Repositories;
using Voting.Email;
using Voting.Email.Models;
using Voting.Sms;

namespace Voting.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly IEmailSender emailSender;
        private readonly ISmsSender smsSender;
        private readonly IUserRepository userRepository;
        private readonly ICountryRepository countryRepository;

        public UserService(IUserRepository userRepository, ICountryRepository countryRepository,
            IEmailSender emailSender, ISmsSender smsSender)
        {
            this.userRepository = userRepository;
            this.countryRepository = countryRepository;
            this.emailSender = emailSender;
            this.smsSender = smsSender;
        }

        public Task<User> FindByAuthTypeAndAuthId(AuthType authType, string authId)
        {
            return userRepository.FindByAuthTypeAndAuthId(authType, authId);
        }

        public Task<User> FindByConfirmationToken(string token)
        {
            return userRepository.FindByConfirmationToken(token);
        }

        public Task<User> FindByForgotPasswordToken(string token)
        {
            return userRepository.FindByForgotPasswordToken(token);
        }

        public Task<User> FindByEmail(string email)
        {
            return userRepository.FindByEmail(email);
        }

        public Task<Country> FindCountry(long id)
        {
            return countryRepository.Find(id);
        }

        public async Task<long> RegisterUser(string email, string password, string lastName, 
            string firstName, string middleName, AuthType? authType, string authId, bool confirmEmail)
        {
            var salt = password == null? null: PasswordHash.CreateSalt();
            var passwordHash = password == null ? null : PasswordHash.CreateHash(salt, password);

            var user = new User
            {
                AuthID = authId,
                AuthType = authType,
                Confirmed = authType.HasValue && !confirmEmail,
                DateJoined = DateTime.UtcNow,
                Deleted = false,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                Password = passwordHash,
                Salt = salt,
                Role = Role.Voter
            };

            user.Id = await userRepository.Add(user);

            if (!authType.HasValue || confirmEmail)
            {
                await SendConfirmationEmail(user);
            }
            return user.Id;
        }

        public async Task SendConfirmationEmail(User user)
        {
            user.ConfirmationToken = PasswordHash.CreateSalt();
            await userRepository.Update(user);

            var mailModel = new AccountConfirmation("Account Confirmation", user.Email,
                ConfigurationManager.AppSettings["WebsiteUrl"] + "#/confirm?token=" + HttpUtility.UrlEncode(user.ConfirmationToken));

            await emailSender.Send(mailModel);
        }

        public Task<int> UpdateUser(User user)
        {
            return userRepository.Update(user);
        }

        public Task<IEnumerable<Country>> GetAllCountries()
        {
            return countryRepository.FindAll();
        }

        public Task<bool> UpdatePhoneNumber(long userId, long countryId, string phoneNumber)
        {
            var user = userRepository.Find(userId).Result;

            if (user == null) return Task.FromResult(false);

            var country = countryRepository.Find(countryId).Result;

            if (country == null) return Task.FromResult(false);

            string number;
            if (phoneNumber.StartsWith("0"))
            {
                number = country.PhoneCode + phoneNumber.Substring(1);
            }
            else
            {
                number = country.PhoneCode + phoneNumber;
            }

            user.MobileNumberCode = user.MobileNumberCode ?? new Random().Next(100000, 999999).ToString();
            var sent = smsSender.Send("Enter " + user.MobileNumberCode + " to confirm your phone number", number).Result;

            if (!sent) return Task.FromResult(false);

            user.Country = country.Id;
            user.MobileNumber = phoneNumber;
            userRepository.Update(user);

            return Task.FromResult(true);

        }

        public Task<bool> ConfirmPhoneNumberCode(long userId, string code)
        {
            var user = userRepository.Find(userId).Result;

            if (user == null) return Task.FromResult(false);

            if (string.IsNullOrWhiteSpace(user.MobileNumberCode) || !user.MobileNumberCode.Equals(code)) return Task.FromResult(false);

            user.MobileNumberConfirmed = true;

            userRepository.Update(user);

            return Task.FromResult(true);
        }

        public Task<bool> ForgotPassword(long userId)
        {
            var user = userRepository.Find(userId).Result;

            if (user == null) return Task.FromResult(false);

            user.ForgotPasswordToken = PasswordHash.CreateSalt();
            user.ForgotPasswordTimeStamp = DateTime.UtcNow;

            userRepository.Update(user);

            var mailModel = new AccountConfirmation("Password Reset", user.Email,
                ConfigurationManager.AppSettings["WebsiteUrl"] + "#/setpassword?token=" + HttpUtility.UrlEncode(user.ForgotPasswordToken));
            emailSender.Send(mailModel);

            return Task.FromResult(true);
        }


        public Task<bool> SetPassword(long userId, string password)
        {
            var user = userRepository.Find(userId).Result;

            if (user == null) return Task.FromResult(false);

            var salt = password == null ? null : PasswordHash.CreateSalt();
            var passwordHash = password == null ? null : PasswordHash.CreateHash(salt, password);

            user.Salt = salt;
            user.Password = passwordHash;
            user.Confirmed = true;

            userRepository.Update(user);
            return Task.FromResult(true);
        }
    }
}
