using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Voting.Common;
using Voting.Data.Models;
using Voting.Models;
using Voting.Models.Results;
using Voting.Service;

namespace Voting.Controllers
{
    public class AccountController : ApiController
    {
        private readonly IUserService userService;


        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("api/account/register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register([FromBody]RegisterModel registerModel)
        {
            if (string.IsNullOrEmpty(registerModel?.FirstName) || string.IsNullOrEmpty(registerModel.Email) || string.IsNullOrEmpty(registerModel.LastName))
            {
                return Content(HttpStatusCode.BadRequest, new Result(ResultCode.INVALID_PARAMETER, "Parameters are empty"));
            }

            if (!Utils.IsValidEmail(registerModel.Email))
            {
                return Content(HttpStatusCode.BadRequest, new Result(ResultCode.INVALID_PARAMETER, "Email is not correctly formatted"));
            }

            if (await userService.FindByEmail(registerModel.Email) != null)
            {
                return Content(HttpStatusCode.BadRequest, new Result(ResultCode.INVALID_PARAMETER, "Email already exists"));
            }

            await userService.RegisterUser(registerModel.Email, registerModel.Password, registerModel.LastName, registerModel.FirstName,
                registerModel.MiddleName, registerModel.AuthType, registerModel.AuthId, registerModel.ConfirmEmail);
            return Ok(new Result(ResultCode.SUCCESS, ""));
        }

        [Route("api/account/confirm")]
        [HttpPost]
        public async Task<IHttpActionResult> Confirm([FromBody] ConfirmModel confimModel)
        {
            User user;
            if (string.IsNullOrEmpty(confimModel.Token) || (user = await userService.FindByConfirmationToken(confimModel.Token)) == null)
            {
                return Content(HttpStatusCode.BadRequest, new Result(ResultCode.INVALID_PARAMETER, "Token is empty or invalid"));
            }

            if (user.Confirmed)
            {
                return Content(HttpStatusCode.BadRequest, new Result(ResultCode.ALREADY_CONFIRMED,
                    "User has already been confirmed."));
            }

            user.Confirmed = true;
            user.DateConfirmed = DateTime.UtcNow;
            await userService.UpdateUser(user);
            return Ok(new Result(ResultCode.SUCCESS, ""));
        }


        [Route("api/account/phone")]
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> RegisterPhoneNumber([FromBody] PhoneModel phoneModel)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).SingleOrDefault();

            var user = await userService.FindByEmail(email);
            if (user.MobileNumberConfirmed)
            {
                return Content(HttpStatusCode.BadRequest, new Result(ResultCode.INVALID_PARAMETER, "Phone number has already been enter"));
            }
            
            if (phoneModel?.Country == null || string.IsNullOrWhiteSpace(phoneModel.PhoneNumber))
            {
                return Content(HttpStatusCode.BadRequest, new Result(ResultCode.INVALID_PARAMETER, "Parameters are empty"));
            }

            var country = userService.FindCountry(phoneModel.Country.Value);
            if(country == null)
            {
                return Content(HttpStatusCode.BadRequest, new Result(ResultCode.INVALID_PARAMETER, "Country is Unknown."));
            }

            if (user.MobileNumber == null || user.MobileNumber != phoneModel.PhoneNumber || user.Country == null || user.Country != phoneModel.Country || user.MobileNumberCode == null)
            {
                var confirmed = await userService.UpdatePhoneNumber(user.Id, phoneModel.Country.Value, phoneModel.PhoneNumber);
                if (!confirmed) return Content(HttpStatusCode.BadRequest, new Result(ResultCode.INVALID_PARAMETER, "Could not save your phone number."));
            }

            if (!string.IsNullOrWhiteSpace(phoneModel.PhoneNumberConfirmationCode) && user.MobileNumber != null && user.Country != null)
            {
                var confirmed = await userService.ConfirmPhoneNumberCode(user.Id, phoneModel.PhoneNumberConfirmationCode);

                if(!confirmed) return Content(HttpStatusCode.BadRequest, new Result(ResultCode.INVALID_PARAMETER, "Could not confirm phone number."));
            }

            return Ok(new Result(ResultCode.SUCCESS, ""));
        }


        [Route("api/account/forgotpassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ForgotPassword([FromBody] ForgotPasswordModel forgotPassword)
        {
            if (string.IsNullOrWhiteSpace(forgotPassword?.Email))
            {
                return Content(HttpStatusCode.BadRequest, new Result(ResultCode.INVALID_PARAMETER, "Email is blank."));
            }

            var user = await userService.FindByEmail(forgotPassword.Email);
            if (user == null)
            {
                return Content(HttpStatusCode.BadRequest, new Result(ResultCode.INVALID_PARAMETER, "User could not be found."));
            }

            if (user.AuthID != null)
            {
                return Content(HttpStatusCode.BadRequest,
                    new Result(ResultCode.INVALID_PARAMETER,
                        "Sorry you logged in with either your Facebook or Twitter account."));
            }

            var forgot = await userService.ForgotPassword(user.Id);
            if(!forgot) return Content(HttpStatusCode.BadRequest, new Result(ResultCode.UNKNOWN, "Could not complete request."));

            return Ok(new Result(ResultCode.SUCCESS, ""));
        }


        [Route("api/account/confirmforgotpasswordtoken")]
        [HttpPost]
        public async Task<IHttpActionResult> ConfirmForgotPasswordToken([FromBody] ConfirmModel confirmModel)
        {
            if (string.IsNullOrWhiteSpace(confirmModel?.Token) || await userService.FindByForgotPasswordToken(confirmModel.Token) == null)
            {
                return Content(HttpStatusCode.BadRequest, new Result(ResultCode.INVALID_PARAMETER, "Token is blank or invalid."));
            }


            return Ok(new Result(ResultCode.SUCCESS, ""));
        }

        [Route("api/account/setpassword")]
        [HttpPost]
        public async Task<IHttpActionResult> SetPassword([FromBody] SetPasswordModel setPasswordModel)
        {
            User user;
            if (string.IsNullOrWhiteSpace(setPasswordModel.Token) || (user = await userService.FindByForgotPasswordToken(setPasswordModel.Token)) == null)
            {
                return Content(HttpStatusCode.BadRequest, new Result(ResultCode.INVALID_PARAMETER, "Token is empty or invalid"));
            }

            if (string.IsNullOrWhiteSpace(setPasswordModel.Password) ||
                string.IsNullOrWhiteSpace(setPasswordModel.ConfirmPassword))
            {
                return Content(HttpStatusCode.BadRequest, new Result(ResultCode.INVALID_PARAMETER, "Password is empty or invalid"));
            }

            if (!setPasswordModel.Password.Equals(setPasswordModel.ConfirmPassword))
            {
                return Content(HttpStatusCode.BadRequest,
                    new Result(ResultCode.INVALID_PARAMETER, "Passwords do not match"));
            }

            var forgot = await userService.SetPassword(user.Id, setPasswordModel.Password);
            if (!forgot) return Content(HttpStatusCode.BadRequest, new Result(ResultCode.UNKNOWN, "Could not complete request."));

            return Ok(new Result(ResultCode.SUCCESS, ""));
        }
    }
}
