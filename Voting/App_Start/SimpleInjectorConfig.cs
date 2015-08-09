using SimpleInjector;
using System.Configuration;
using System.Web;
using System.Web.Http;
using Voting.Data;
using Voting.Data.Repositories;
using Voting.Data.Repositories.Impl;
using Voting.Email;
using Voting.Service;
using Voting.Service.Impl;
using Voting.Sms;
using Voting.Sms.Impl;

namespace Voting
{
    public static class SimpleInjectorConfig
    {

        public static Container Register()
        {
            var container = new Container();

            var connectionSettings = ConfigurationManager.ConnectionStrings["default"];
            container.RegisterSingle<IDatabaseConnection>(() => new DatabaseConnection(connectionSettings.ConnectionString));
            container.RegisterSingle<IDataAccess, DataAccess>();
            container.RegisterSingle<IEmailSender>(() => new EmailSender());
            container.RegisterSingle<ISmsSender>(() => new SmsSender(ConfigurationManager.AppSettings["TwilioAccountSID"], ConfigurationManager.AppSettings["TwilioAuthToken"]));
            container.RegisterSingle<IUserRepository, UserRepository>();
            container.RegisterSingle<ICountryRepository, CountryRepository>();
            container.RegisterSingle<IPositionRepository, PositionRepository>();
            container.RegisterSingle<ICandidateRepository, CandidateRepository>();
            container.RegisterSingle<IVoteRepository, VoteRepository>();
            container.RegisterSingle<IUserService, UserService>();
            container.RegisterSingle<IVoteService, VoteService>();

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();

            return container;
        }

    }
}
