
namespace OneMoreLadder.DataAccess.DataModel
{

    internal class Match
    {
        public int MatchId { get; set; }
        public DateTime Date { get; set; }

        public  Player? HomePlayer { get; set; }

        public Player? GuestPlayer { get; set; }

        public int HomePlayerId { get; set; }

        public int GuestPlayerId { get; set; }

        public int FirstSetHomePlayerScore { get; set; }

        public int FirstSetGuestPlayerScore { get; set; }

        public int SecondSetHomePlayerScore { get; set; }

        public int SecondSetGuestPlayerScore { get; set; }

        public int ThirdSetHomePlayerScore { get; set; }

        public int ThirdSetGuestPlayerScore { get; set; }

    }
}
