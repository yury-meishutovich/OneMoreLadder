using OneMoreLadder.DataAccess.DataModel;

namespace OneMoreLadder.DataAccess.Records
{
    public class PlayerRecord
    {
        internal PlayerRecord(Player player)
        {
            PlayerId = player.PlayerId;
            FirstName = player.FirstName;
            LastName = player.LastName;            
        }


        public int PlayerId { get; }

        public string FirstName { get; }

        public string LastName { get; }       
    }
}
