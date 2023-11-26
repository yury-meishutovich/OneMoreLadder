using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneMoreLadder.DataAccess.Contracts;
using OneMoreLadder.DataAccess.Records;

namespace OneMoreLadder.DataAccess.Repositories
{
    public class MatchesRepository : Repository<MatchesRepository>, IMatchesRepository
    {
        public MatchesRepository(IDataAccessSettings dataAccessSettings, ILogger<MatchesRepository> logger) : base(dataAccessSettings, logger)
        {
        }

        public async Task<MatchRecord[]> GetMatchesAsync(CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async (db) =>
            {
                return await db.Matches.OrderByDescending(m => m.Date).Include(h=>h.HomePlayer).Include(g=>g.GuestPlayer).Select(m => new MatchRecord(m)).ToArrayAsync(cancellationToken);
            });

        }
    }
}
