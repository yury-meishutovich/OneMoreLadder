namespace OneMoreLadder.DataAccess.DataModel
{    
    internal class Player
    {        
        public int PlayerId { get; set; }
        
        public Guid SecureId { get; set; }                        
        
        public required string FirstName { get; set; }
                
        public required string LastName { get; set; } 
        
    }
}
