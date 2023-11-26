using OnreMoreLadder.BusinessLogic.Model;

namespace OnreMoreLadder.BusinessLogic.Contracts
{
    public interface IChallenges
    {
        Task<int> AddAsync(int loginPlayerId, int guestPlayerId, CancellationToken cancellation);        
        Task ReportScoreAsync(int loginPlayerId, Score score, CancellationToken cancellationToken);
        Task<IEnumerable<Challenge>> GetPlayerChallengesAsync(int loginPlayerId, CancellationToken cancellation);
    }
}
