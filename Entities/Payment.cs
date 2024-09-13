namespace Luce
{
    public class Payment : BaseEntity
    {
        public DateTime Date {get; set;}
        public double Amount {get; set;}
        // public int OrderId {get; set;}
        public Order Order {get; set;}
        public PaymentStatus Status {get; set;}
    }
}