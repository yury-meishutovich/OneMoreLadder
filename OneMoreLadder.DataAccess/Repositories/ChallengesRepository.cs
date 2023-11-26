using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneMoreLadder.DataAccess.Contracts;
using OneMoreLadder.DataAccess.DataModel;
using OneMoreLadder.DataAccess.Records;


namespace OneMoreLadder.DataAccess.Repositories
{
    public class ChallengesRepository : Repository<ChallengesRepository>, IChallengesRepository
    {
        public ChallengesRepository(IDataAccessSettings dataAccessSettings, ILogger<ChallengesRepository> logger) : base(dataAccessSettings, logger)
        {
        }

        public async Task<int> AddChallengeAsync(int homePlayerId, int guestPlayerId, CancellationToken cancellationToken)
        {                        
            return await ExecuteAsync(async (db) =>
            {

                Challenge challenge = new()
                {
                    HomePlayerId = homePlayerId,
                    GuestPlayerId = guestPlayerId,                    
                };

                await db.Challenges.AddAsync(challenge);
                await db.SaveChangesAsync(cancellationToken);

                return challenge.ChallengeId;
            });
        }

        public async Task<ChallengeRecord> GetChallengeByIdAsync(int challengeId, CancellationToken cancellationToken)
        {
            return await ExecuteAsync<ChallengeRecord>(async (db) =>
            {
                var c = await db.Challenges.Include(h => h.HomePlayer).Include(g => g.GuestPlayer).FirstOrDefaultAsync(c => c.ChallengeId == challengeId);
                return new ChallengeRecord(c);
            });
        }

        public async Task<ChallengeRecord[]> GetChallengeRecordsAsync(int playerId, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async (db) =>
            {
                return await db.Challenges.Where(c => c.GuestPlayerId == playerId || c.HomePlayerId == playerId).Include(h => h.HomePlayer).Include(g => g.GuestPlayer)                
                .Select(c => new ChallengeRecord(c))
                .ToArrayAsync(cancellationToken);                 
            });
        }


        public async Task ReportScoreAsync(int challengeId, int homePlayerFirstSet, 
            int guestPlayerFirstSet, int homePlayerSecondSet, 
            int guestPlayerSecondSet, int homePlayerThirdSet, 
            int guestPlayerThirdSet, DateTime date, CancellationToken cancellationToken)
        {
            await ExecuteAsync(async (db) =>
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var challenge = await db.Challenges.Where(c => c.ChallengeId == challengeId).FirstOrDefaultAsync(cancellationToken);
                    if (challenge == null)
                        throw new Exception($"Cannot find challenge. id: {challengeId}");

                    Match match = new()
                    {
                        GuestPlayerId = challenge.GuestPlayerId,
                        HomePlayerId = challenge.HomePlayerId,
                        FirstSetHomePlayerScore = homePlayerFirstSet,
                        FirstSetGuestPlayerScore = guestPlayerFirstSet,
                        SecondSetHomePlayerScore = homePlayerSecondSet,
                        SecondSetGuestPlayerScore = guestPlayerSecondSet,
                        ThirdSetHomePlayerScore = homePlayerThirdSet,
                        ThirdSetGuestPlayerScore = guestPlayerThirdSet,
                        Date = date
                    };

                    await db.Matches.AddAsync(match, cancellationToken);                    
                    db.Remove(challenge);
                    await db.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync();
                }
               
            });
        }
        
    }
}
