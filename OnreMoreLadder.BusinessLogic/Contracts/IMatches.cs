using OnreMoreLadder.BusinessLogic.Model;

namespace OnreMoreLadder.BusinessLogic.Contracts
{
    public interface IMatches
    {
        Task<IEnumerable<Match>> GetMatchesAsync(CancellationToken cancellationToken);
    }
}
