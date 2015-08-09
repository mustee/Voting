using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using SimpleInjector.Integration.WebApi;
using Voting.Service;
using Voting.Provider;

[assembly: OwinStartup(typeof(Voting.Startup))]

namespace Voting
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            // token consumption
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            var container = SimpleInjectorConfig.Register();
            var config = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };
            WebApiConfig.Register(config);
            app.UseWebApi(config);

            Func<IUserService> userServiceFactory = () => container.GetInstance<IUserService>();
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                // for demo purposes
                AllowInsecureHttp = true,

                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(8),

                Provider = new AuthorizationServerProvider(userServiceFactory)
            });
        }
    }
}
