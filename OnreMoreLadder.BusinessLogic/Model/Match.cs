using OneMoreLadder.DataAccess.Records;

namespace OnreMoreLadder.BusinessLogic.Model
{
    public class Match
    {
        public Match(MatchRecord record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            Date = record.Date;
            HomePlayerName = $"{record.HomePlayerFirstName} {record.HomePlayerLastName}";
            GuestPlayerName = $"{record.GuestPlayerFirstName} {record.GuestPlayerLastName}";
            HomePlayerScore = [record.FirstSetHomePlayerScore, record.SecondSetHomePlayerScore, record.ThirdSetHomePlayerScore];
            GuestPlayerScore = [record.FirstSetGuestPlayerScore, record.SecondSetGuestPlayerScore, record.ThirdSetGuestPlayerScore];
        }
        public DateTime Date { get; }

        public string HomePlayerName { get; }

        public string GuestPlayerName { get; }

        public int[] HomePlayerScore { get; }

        public int[] GuestPlayerScore { get; }
    }
}
