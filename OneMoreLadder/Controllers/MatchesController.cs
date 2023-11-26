using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnreMoreLadder.BusinessLogic.Contracts;

namespace OneMoreLadder.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class MatchesController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IMatches _matches;

        public MatchesController(IMatches matches, ILogger<PlayersController> logger) 
        { 
            _matches = matches;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var res = await _matches.GetMatchesAsync(cancellationToken);

                var response = res.Select(r => new
                {
                    r.HomePlayerName,
                    r.GuestPlayerName,
                    r.Date,
                    r.HomePlayerScore,
                    r.GuestPlayerScore
                }).ToArray();

                return Ok(response);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Cannot get matches");
                return StatusCode(500);
            }
        }
    }
}
