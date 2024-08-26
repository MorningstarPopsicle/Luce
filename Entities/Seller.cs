namespace Luce
{
    public class Seller : BaseEntity
    {
        public int UserId {get; set;}
        public User User {get; set;}
        public bool IsVerified {get; set;}
        public string Logo {get; set;}
        public string StoreName {get; set;}
        public string AccountNumber {get; set;}
        public Address Address {get; set;}
        public List<Product> Products {get; set;}
        public List<Order> Orders {get; set;}
       public List<SellerDispatch> SellerDispatches {get; set;}
        public List<Payment> Payments {get; set;}
    }
}