using Microsoft.Extensions.Logging;
using OneMoreLadder.DataAccess.Contracts;
using OnreMoreLadder.BusinessLogic.Contracts;
using OnreMoreLadder.BusinessLogic.Model;

namespace OnreMoreLadder.BusinessLogic
{
    public class Matches : IMatches
    {
        private readonly IMatchesRepository _matchesRepository;
        private readonly ILogger<Matches> _logger;


        public Matches(IMatchesRepository matchesRepository, ILogger<Matches> logger)
        {
            _matchesRepository = matchesRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Match>> GetMatchesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var matchesRecords = await _matchesRepository.GetMatchesAsync(cancellationToken);
                return matchesRecords.Select(r => new Match(r)).ToArray();
            }catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot get matches");
                throw;
            }
        }
    }
}
