
namespace OneMoreLadder.DataAccess.DataModel
{

    internal class Challenge
    {        
        public int ChallengeId { get; set; }

        public  Player? HomePlayer { get; set; }
        
        public int HomePlayerId { get; set; }

        public Player? GuestPlayer { get; set; }
        
        public int GuestPlayerId { get; set; }              
    }
}
