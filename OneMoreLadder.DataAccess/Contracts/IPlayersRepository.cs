using OneMoreLadder.DataAccess.Records;

namespace OneMoreLadder.DataAccess.Contracts
{
    public interface IPlayersRepository
    {
        Task<PlayerRecord[]> GetPlayersAsync(CancellationToken cancellationToken);

        Task<int> AddPlayerAsync(Guid secureId, string firstName, string lastName, DateTime joinedDate, int wildCard, CancellationToken cancellationToken);

        Task<PlayerRecord> GetPlayerBySecureIdAsync(Guid secureId, CancellationToken cancellationToken);
    }
}
