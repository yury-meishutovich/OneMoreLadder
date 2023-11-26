using OnreMoreLadder.BusinessLogic.Model;

namespace OnreMoreLadder.BusinessLogic.Contracts
{
    public interface IPlayers
    {
        Task<IEnumerable<Player>> GetPlayersAsync(CancellationToken cancellationToken);

        Task<int> GetPlayerInternalIdAsync(Guid secureId, CancellationToken cancellationToken);

    }
}
