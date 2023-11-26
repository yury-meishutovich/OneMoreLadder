using OneMoreLadder.DataAccess.DataModel;

namespace OneMoreLadder.DataAccess.Records
{
    public class ChallengeRecord
    {
        internal ChallengeRecord(Challenge? challenge)
        {
            if (challenge == null)
                throw new ArgumentNullException(nameof(challenge));

            if (challenge.HomePlayer == null)
                throw new ArgumentNullException(nameof(challenge.HomePlayer));

            if (challenge.GuestPlayer == null)
                throw new ArgumentNullException(nameof(challenge.GuestPlayer));

            HomePlayerFirstName = challenge.HomePlayer.FirstName;
            HomePlayerLastName = challenge.HomePlayer.LastName;
            GuestPlayerFirstName = challenge.GuestPlayer.FirstName;
            GuestPlayerLastName = challenge.GuestPlayer.LastName;
            HomePlayerId = challenge.HomePlayerId;
            GuestPlayerId = challenge.GuestPlayerId;
            ChallengeId = challenge.ChallengeId;
            
        }
                
        public string HomePlayerFirstName { get; }

        public string HomePlayerLastName { get; }

        public string GuestPlayerFirstName { get; }

        public string GuestPlayerLastName { get; }

        public int HomePlayerId { get; }

        public int GuestPlayerId { get; }
        
        public int ChallengeId { get; }
    }
}
