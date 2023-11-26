using Microsoft.Extensions.Logging;
using OneMoreLadder.DataAccess.Contracts;
using OnreMoreLadder.BusinessLogic.Contracts;
using OnreMoreLadder.BusinessLogic.Model;

namespace OnreMoreLadder.BusinessLogic
{
    public class Challenges : IChallenges
    {

        private readonly IChallengesRepository _challengesRepository;
        private readonly ILogger<Challenges> _logger;

        public Challenges(IChallengesRepository challengesRepository, ILogger<Challenges> logger)
        {
            _challengesRepository = challengesRepository;
            _logger = logger;
        }

        public async Task<int> AddAsync(int loginPlayerId, int guestPlayerId, CancellationToken cancellation)
        {
            try
            {
                return await _challengesRepository.AddChallengeAsync(loginPlayerId, guestPlayerId, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot add challenge");
                throw;
            }
        }


        public async Task<IEnumerable<Challenge>> GetPlayerChallengesAsync(int loginPlayerId, CancellationToken cancellation)
        {
            try
            {
                var challengeRecords = await _challengesRepository.GetChallengeRecordsAsync(loginPlayerId, cancellation);
                return challengeRecords.Select(c => new Challenge(c)).ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Cannot get player challanges. Player id:{loginPlayerId}");
                throw;
            }
        }

        public async Task ReportScoreAsync(int loginPlayerId, Score score, CancellationToken cancellationToken)
        {

            try
            {
                if (score == null)
                    throw new ArgumentNullException(nameof(score));


                var challenge = await _challengesRepository.GetChallengeByIdAsync(score.ChallangeId, cancellationToken);

                if (challenge == null)
                    throw new Exception($"Cannot get challenge by id: {score.ChallangeId}");

                if (challenge.HomePlayerId != loginPlayerId && challenge.GuestPlayerId != loginPlayerId)
                    throw new Exception($"Player {loginPlayerId} cannot report score for challenge {score.ChallangeId}");

                await _challengesRepository.ReportScoreAsync(score.ChallangeId,
                    score.HomePlayerScore[0],
                    score.GuestPlayerScore[0],
                    score.HomePlayerScore[1],
                    score.GuestPlayerScore[1],
                    score.HomePlayerScore[2],
                    score.GuestPlayerScore[2],
                    score.Date, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot report score");
                throw;
            }
        }
    }
}