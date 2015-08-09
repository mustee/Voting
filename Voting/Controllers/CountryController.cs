using System.Threading.Tasks;
using System.Web.Http;
using Voting.Models.Results;
using Voting.Service;
using System.Linq;
using Voting.Models;

namespace Voting.Controllers
{
    public class CountryController: ApiController
    {
        private readonly IUserService userService;
        public CountryController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("api/country/all")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetAll()
        {
            var countries = await userService.GetAllCountries();

            return
                Ok(new ListResult<CountryModel>(
                    countries.Select(c => new CountryModel {Id = c.Id, Name = c.Name, PhoneCode = c.PhoneCode}),
                    ResultCode.SUCCESS, ""));
        }
    }
}
