using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneMoreLadder.DataAccess.Contracts;
using OneMoreLadder.DataAccess.DataModel;
using OneMoreLadder.DataAccess.Records;

namespace OneMoreLadder.DataAccess.Repositories
{
    public class PlayersRepository :  Repository<PlayersRepository>, IPlayersRepository
    {
        public PlayersRepository(IDataAccessSettings dataAccessSettings, ILogger<PlayersRepository> logger) : base(dataAccessSettings, logger)
        {
        }

        public async Task<PlayerRecord[]> GetPlayersAsync(CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async (db) =>
            {

                return await db.Players.Select(p => new PlayerRecord(p)).ToArrayAsync(cancellationToken);
            });
        
        
        }

        public async Task<int> AddPlayerAsync(Guid secureId, string firstName, string lastName, DateTime joinedDate, int wildCard, CancellationToken cancellationToken)
        {
            if (Guid.Empty == secureId)
                throw new ArgumentNullException(nameof(secureId));

            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof(lastName));


            return await ExecuteAsync(async (db) =>
            {
                Player player = new()
                {
                    SecureId = secureId,
                    FirstName = firstName,
                    LastName = lastName
                    
                };

                await db.Players.AddAsync(player, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return player.PlayerId;
            });
        }

        public async Task<PlayerRecord> GetPlayerBySecureIdAsync(Guid secureId, CancellationToken cancellationToken)
        {
            return await ExecuteAsync<PlayerRecord>(async (db) =>
            {
                var res = await db.Players.FirstAsync(p => p.SecureId == secureId, cancellationToken);
                                
                return  new PlayerRecord(res);
            });
        }
    }
}
