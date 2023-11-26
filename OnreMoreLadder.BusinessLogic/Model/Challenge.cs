using OneMoreLadder.DataAccess.Records;


namespace OnreMoreLadder.BusinessLogic.Model
{
    public class Challenge
    {
        internal Challenge(ChallengeRecord challengeRecord)
        {
            if (challengeRecord == null)
                throw new ArgumentNullException(nameof(challengeRecord));
            
            Id = challengeRecord.ChallengeId;
            HomePlayerName = $"{challengeRecord.HomePlayerFirstName} {challengeRecord.HomePlayerLastName}";
            GuestPlayerName = $"{challengeRecord.GuestPlayerFirstName} {challengeRecord.GuestPlayerLastName}";            
        }



        public int Id { get; }
        
        public string HomePlayerName { get; }

        public string GuestPlayerName { get; }        
    }
}
