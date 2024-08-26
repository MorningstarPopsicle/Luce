namespace Luce
{
    public class Dispatch : BaseEntity
    {
        public List<SellerDispatch> SellerDispatches {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
        
    }
}