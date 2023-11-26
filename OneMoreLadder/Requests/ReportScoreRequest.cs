namespace OneMoreLadder.Requests
{
    public class ReportScoreRequest
    {
        public int ChallengeId { get; set; }

        public int[]? HomePlayerScore { get; set; }

        public int[]? GuestPlayerScore { get; set; }
    }
}
