using OneMoreLadder.DataAccess.DataModel;

namespace OneMoreLadder.DataAccess.Records
{
    public class MatchRecord
    {
        internal MatchRecord(Match match)
        {
            if (match == null) 
                throw new ArgumentNullException("match");

            if (match.HomePlayer == null)
                throw new ArgumentNullException(nameof(match.HomePlayer));

            if (match.GuestPlayer == null)
                throw new ArgumentNullException(nameof(match.GuestPlayer));

            HomePlayerFirstName = match.HomePlayer.FirstName;
            HomePlayerLastName = match.HomePlayer.LastName;
            GuestPlayerFirstName = match.GuestPlayer.FirstName;
            GuestPlayerLastName = match.GuestPlayer.LastName;
            FirstSetHomePlayerScore = match.FirstSetHomePlayerScore;
            FirstSetGuestPlayerScore = match.FirstSetGuestPlayerScore;
            SecondSetHomePlayerScore = match.SecondSetHomePlayerScore;
            SecondSetGuestPlayerScore = match.SecondSetGuestPlayerScore;
            ThirdSetHomePlayerScore = match.ThirdSetHomePlayerScore;
            ThirdSetGuestPlayerScore = match.ThirdSetGuestPlayerScore;
            Date = match.Date;

        }

        public DateTime Date { get; }

        public string HomePlayerFirstName { get; }

        public string HomePlayerLastName { get; }

        public string GuestPlayerFirstName { get; }

        public string GuestPlayerLastName { get; }

        public int FirstSetHomePlayerScore { get; }

        public int FirstSetGuestPlayerScore { get; }

        public int SecondSetHomePlayerScore { get; }

        public int SecondSetGuestPlayerScore { get; }

        public int ThirdSetHomePlayerScore { get; }

        public int ThirdSetGuestPlayerScore { get; }
    }
}
