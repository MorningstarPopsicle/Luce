namespace Luce.DTOs
{
    public class PaymentDto
    {
        public DateTime Date {get; set;}
        public int Id {get; set;}
        public double Amount {get; set;}
        public PaymentStatus Status {get; set;}
    }
}