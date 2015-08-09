using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Voting.Data.Models;
using Voting.Models;
using Voting.Models.Results;
using Voting.Service;

namespace Voting.Controllers
{
    public class VoteController : ApiController
    {
        private readonly IUserService userService;
        private readonly IVoteService voteService;
        public VoteController(IUserService userService, IVoteService voteService)
        {
            this.userService = userService;
            this.voteService = voteService;
        }


        [Route("api/vote/positions")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetPositions()
        {
            var positions = await voteService.GetPositions();
            return Ok(new ListResult<Position>(positions.OrderBy(p => p.Id), ResultCode.SUCCESS, ""));
        }


        [Route("api/vote/candidates")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetCandidates(long positionId)
        {
            var candidates = await voteService.GetCandidatesByPosition(positionId);
            return
                Ok(
                    new ListResult<CandidateModel>(
                        candidates.Select(c => new CandidateModel {Id = c.Id, Name = c.FullName}).OrderBy(c => c.Id),
                        ResultCode.SUCCESS, ""));
        }


        [Route("api/vote")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetResult(long positionId)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).SingleOrDefault();

            var user = await userService.FindByEmail(email);
            var vote = await voteService.GetUserVote(user.Id, positionId);

            if (vote == null)
            {
                return Ok(new Result(ResultCode.SUCCESS, ""));
            }

            return
                Ok(
                    new ItemResult<VoteModel>(
                        new VoteModel {Id = vote.Id, CandidateId = vote.CandidateId},
                        ResultCode.SUCCESS, ""));
        }


        [Route("api/vote")]
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> Vote([FromBody] CandidateModel candidateModel)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).SingleOrDefault();

            var user = await userService.FindByEmail(email);
            var candidate = await voteService.FindCandidate(candidateModel.Id);
            if (candidate == null)
            {
                return Content(HttpStatusCode.BadRequest, new Result(ResultCode.INVALID_PARAMETER, "Candidate not found."));
            }

            var vote = await voteService.GetUserVote(user.Id, candidate.PositionId);
            if (vote != null)
            {
                return Content(HttpStatusCode.BadRequest, new Result(ResultCode.ALREADY_VOTED, "You have already voted for this position."));
            }

            await voteService.AddVote(user.Id, candidate.Id, DateTime.UtcNow);
            return Ok(new Result(ResultCode.SUCCESS, ""));
        }
    }
}
