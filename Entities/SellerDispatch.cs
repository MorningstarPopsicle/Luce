namespace Luce
{
    public class SellerDispatch : BaseEntity
    {
        public int SellerId {get; set;}
        public Seller Seller {get; set;}
        public int DispatchId {get; set;}
        public Dispatch Dispatch {get; set;}
        
    }
}