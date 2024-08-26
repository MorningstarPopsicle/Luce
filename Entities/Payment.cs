namespace Luce
{
    public class Payment : BaseEntity
    {
        public DateTime Date {get; set;}
        public int SenderId {get; set;}
        public Customer Customer {get; set;}
        public int ReceiverId {get; set;}
        public Seller Seller {get; set;}
        // public int AdminId {get; set;}
        // public Admin Admin {get; set;}
        public double Amount {get; set;}
        public Order Order {get; set;}
        public PaymentStatus Status {get; set;}
    }
}