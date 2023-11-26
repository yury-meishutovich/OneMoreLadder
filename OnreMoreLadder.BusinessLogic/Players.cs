using Microsoft.Extensions.Logging;
using OneMoreLadder.DataAccess.Contracts;
using OnreMoreLadder.BusinessLogic.Contracts;
using OnreMoreLadder.BusinessLogic.Model;

namespace OnreMoreLadder.BusinessLogic
{
    public class Players : IPlayers
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly ILogger<Players> _logger;
        public Players(IPlayersRepository playersRepository, ILogger<Players> logger)
        {
            _playersRepository = playersRepository;
            _logger = logger;
            
        }

        public async Task<int> GetPlayerInternalIdAsync(Guid secureId, CancellationToken cancellationToken)
        {
            try
            {
                var playerRecord = await _playersRepository.GetPlayerBySecureIdAsync(secureId, cancellationToken);
                return playerRecord.PlayerId;
            }catch (Exception ex) {
                _logger.LogError(ex, $"Cannot get internal player id. SecureId: {secureId}");
                throw;
            }
        }

        public async Task<IEnumerable<Player>> GetPlayersAsync(CancellationToken cancellationToken)
        {
            try
            {
                var playerRecords = await _playersRepository.GetPlayersAsync(cancellationToken);
                return playerRecords.Select(r => new Player(r)).ToArray();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Cannot get players");
                throw;
            }
        }        
    }
}
