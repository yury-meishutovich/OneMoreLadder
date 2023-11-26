using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneMoreLadder.Requests;
using OnreMoreLadder.BusinessLogic.Contracts;
using OnreMoreLadder.BusinessLogic.Model;

namespace OneMoreLadder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ChallengesController : ControllerBase
    {
        private const string IdentityProviderUserIdClaim = "IdentityProviderUserIdClaim";

        private readonly ILogger<ChallengesController> _logger;

        private readonly IChallenges _challenges;

        private readonly IPlayers _players;

        private readonly IConfiguration _configuration;

        private async Task<int> GetInternalPlayerIdAsync(CancellationToken cancellationToken)
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == _configuration[IdentityProviderUserIdClaim]);

            if (claim == null)
                throw new Exception("Cannot get user id from http context");

            return await _players.GetPlayerInternalIdAsync(Guid.Parse(claim.Value), cancellationToken);

        }

        public ChallengesController(IConfiguration configuration, ILogger<ChallengesController> logger, IChallenges challenges, IPlayers players)
        {
            _configuration = configuration;
            _logger = logger;
            _challenges = challenges;
            _players = players;
        }


        [HttpPost]
        [Route("ReportScore")]
        public async Task<IActionResult> ReportScore(ReportScoreRequest reportScoreRequest, CancellationToken cancellationToken)
        {            
            Score? score = null;

            try
            {
                score = new Score(reportScoreRequest.ChallengeId, reportScoreRequest.HomePlayerScore, reportScoreRequest.GuestPlayerScore, DateTime.UtcNow);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "ReportScore has bad request");
                return BadRequest();
            }


            try
            {
                int loggedPlayerId = await GetInternalPlayerIdAsync(cancellationToken);


                await _challenges.ReportScoreAsync(loggedPlayerId,
                score, cancellationToken);
             
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot report score");                
                return StatusCode(500);
            }

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> Post(IssueChallengeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                int loggedPlayerId = await GetInternalPlayerIdAsync(cancellationToken);

                await _challenges.AddAsync(loggedPlayerId, request.PlayerId, cancellationToken);
                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot issue challenge");
                return StatusCode(500);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                int loggedPlayerId = await GetInternalPlayerIdAsync(cancellationToken);
                var res = await _challenges.GetPlayerChallengesAsync(loggedPlayerId, cancellationToken);

                var response = res.Select(r => new
                {
                    r.HomePlayerName,
                    r.GuestPlayerName,
                    r.Id
                }).ToArray();

                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Cannot get challanges");
                return StatusCode(500);
            }
        }
    }
}