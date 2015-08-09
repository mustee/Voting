using Microsoft.Owin.Security.OAuth;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Voting.Common;
using Voting.Data.Models;
using Voting.Data.Models.Enums;
using Voting.Service;

namespace Voting.Provider
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly Func<IUserService> userServiceFactory;
        public AuthorizationServerProvider(Func<IUserService> userServiceFactory)
        {
            this.userServiceFactory = userServiceFactory;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // OAuth2 supports the notion of client authentication
            // this is not used here
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            // validate user credentials (demo!)
            // user credentials should be stored securely (salted, iterated, hashed yada)
            if (string.IsNullOrWhiteSpace( context.UserName) || string.IsNullOrWhiteSpace(context.Password))
            {
                context.Rejected();
                return Task.FromResult<object>(null);
            }

            var userService = userServiceFactory.Invoke();
            User user;
            int authType;
            if(((user = userService.FindByEmail(context.UserName).Result) == null || !PasswordHash.ValidatePassword(user.Salt, context.Password, user.Password))
                && (!int.TryParse(context.UserName, out authType) || (user = userService.FindByAuthTypeAndAuthId((AuthType)authType, context.Password).Result) == null))
            {
                context.Rejected();
                return Task.FromResult<object>(null);
            }

            // create identity
            var id = new ClaimsIdentity(context.Options.AuthenticationType);
            if (user.Confirmed)
            {
                id.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                id.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));
            }

            if (user.MobileNumber != null)
                id.AddClaim(new Claim(ClaimTypes.MobilePhone, user.MobileNumber));

            var country = user.Country.HasValue ? userService.FindCountry(user.Country.Value).Result : null;
            if (country != null)
                id.AddClaim(new Claim(ClaimTypes.Country, country.Id.ToString()));

            if (user.MobileNumberConfirmed)
                id.AddClaim(new Claim("MobileNumberConfirmed", user.MobileNumberConfirmed.ToString()));

            context.Validated(id);
            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var claim in context.Identity.Claims)
            {
                if (claim.Type == ClaimTypes.Email)
                {
                    context.AdditionalResponseParameters.Add("email", claim.Value);
                }

                if (claim.Type == ClaimTypes.Role)
                {
                    context.AdditionalResponseParameters.Add("role", claim.Value);
                }

                if (claim.Type == ClaimTypes.MobilePhone)
                {
                    context.AdditionalResponseParameters.Add("mobile_number", claim.Value);
                }

                if (claim.Type == ClaimTypes.Country)
                {
                    context.AdditionalResponseParameters.Add("country", claim.Value);
                }

                if(claim.Type == "MobileNumberConfirmed")
                {
                    context.AdditionalResponseParameters.Add("mobile_number_confirmed", claim.Value);
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}
