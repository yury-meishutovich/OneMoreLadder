using OneMoreLadder.DataAccess.Records;

namespace OneMoreLadder.DataAccess.Contracts
{
    public interface IMatchesRepository
    {
        Task<MatchRecord[]> GetMatchesAsync(CancellationToken cancellationToken);        
    }
}
