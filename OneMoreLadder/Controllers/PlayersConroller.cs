using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnreMoreLadder.BusinessLogic.Contracts;


namespace OneMoreLadder.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IPlayers _players;

        public PlayersController(ILogger<PlayersController> logger, IPlayers players)
        {
            _logger = logger;
            _players = players;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var players = await _players.GetPlayersAsync(cancellationToken);

                int pos = 1;                

                return Ok(players.Select(p => new
                {
                    p.Id,
                    p.Name,
                    Pos = pos++

                }).ToArray());

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot get players list");
                return StatusCode(500);
            }
        }
    }
}
