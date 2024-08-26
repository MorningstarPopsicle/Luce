namespace Luce
{
    public class Product : BaseEntity
    {
        public double Price {get; set;}
        public string ImageUrl {get; set;}
        public int Quantity {get; set;}
        public int InitialQuantity {get; set;}
        public string ProductName {get; set;}
        public int SellerId {get; set;}
        public Seller Seller {get; set;}
        public List<Review> Reviews {get; set;}
        
    }
}