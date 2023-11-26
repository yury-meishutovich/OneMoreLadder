using OneMoreLadder.DataAccess.Records;

namespace OnreMoreLadder.BusinessLogic.Model
{
    public class Player
    {
        internal Player(PlayerRecord playerRecord)
        {
            if (playerRecord == null)
                throw new ArgumentNullException(nameof(playerRecord));
            
            Id = playerRecord.PlayerId;
            Name = $"{playerRecord.FirstName} {playerRecord.LastName}";
        }

        public int Id { get; }

        public string Name { get; }        
    }
}
