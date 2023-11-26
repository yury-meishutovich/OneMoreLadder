namespace OnreMoreLadder.BusinessLogic.Model
{

    public class Score
    {
        public int ChallangeId { get; }
        public int[] HomePlayerScore { get; }
        public int[] GuestPlayerScore { get; }
        
        public DateTime Date { get; }

        public Score(int challangeId, int[]? homePlayerScore, int[]? guestPlayerScore, DateTime date)
        {
            if (homePlayerScore == null || homePlayerScore.Length != 3)
                throw new ArgumentException(nameof(homePlayerScore));

            if (guestPlayerScore == null || guestPlayerScore.Length != 3)
                throw new ArgumentException(nameof(guestPlayerScore));

            ChallangeId = challangeId;
            HomePlayerScore = homePlayerScore;
            GuestPlayerScore = guestPlayerScore;
            Date = date;
        }
    }
}
