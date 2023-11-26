using OneMoreLadder.DataAccess.Records;

namespace OneMoreLadder.DataAccess.Contracts
{
    public interface IChallengesRepository
    {

        Task<int> AddChallengeAsync(int homePlayerId, int guestPlayerId, CancellationToken cancellationToken);

        Task<ChallengeRecord[]> GetChallengeRecordsAsync(int playerId, CancellationToken cancellationToken);

        Task ReportScoreAsync(int challengeId, int homePlayerFirstSet,
               int guestPlayerFirstSet, int homePlayerSecondSet,
               int guestPlayerSecondSet, int homePlayerThirdSet,
               int guestPlayerThirdSet, DateTime date, CancellationToken cancellationToken);

        Task<ChallengeRecord> GetChallengeByIdAsync(int challengeId, CancellationToken cancellationToken);
    }
}
