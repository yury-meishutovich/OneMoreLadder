using Moq;
using Microsoft.Extensions.Logging;
using OnreMoreLadder.BusinessLogic;
using OneMoreLadder.DataAccess.Contracts;

namespace OneMoreLadder.Tests
{
    public class TestsChallenges
    {
        [Test]
        public async Task TestAddChallengeAsyncCalled()
        {
            var mockLogger = new Mock<ILogger<Challenges>>();
            var mockLChallengeRepo = new Mock<IChallengesRepository>();
            CancellationToken ct = new CancellationToken();
            mockLChallengeRepo.Setup(c=>c.AddChallengeAsync(1, 2, ct)).Returns(Task.FromResult(1)); 

            Challenges challenges = new Challenges(mockLChallengeRepo.Object, mockLogger.Object);
            var res = await challenges.AddAsync(1, 2, ct);

            Assert.IsTrue(res == 1);

        }
    }
}