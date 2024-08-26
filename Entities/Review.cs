namespace Luce
{
    public class Review : BaseEntity
    {
        public string Message {get; set;}
        public int NoOfStars {get; set;}
        public int CustomerId {get; set;}
        public Customer Customer {get; set;}
        public Product Product {get; set;}
        public int ProductId {get; set;}
    }
}