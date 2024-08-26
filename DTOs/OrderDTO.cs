namespace Luce.DTOs
{
    public class OrderDto
    {
        public int Id {get; set;}
        public DateTime Date {get; set;}
        public CustomerDto CustomerDto {get; set;}
        public int PaymentId {get; set;}
        public PaymentDto PaymentDto {get; set;}
        public SellerDto SellerDto {get; set;}
        // public double[] Positions {get; set;}
        public double Distance {get; set;}
        public int RateValue {get; set;}
        public bool DeliveryIsVerifiedByCustomer {get; set;}
        public bool DeliveryIsVerifiedBySeller {get; set;}
        public string RefNo {get; set;}
        public double TotalPrice {get; set;}
        public CartItemDto ItemDto {get; set;}
        public AddressDto DeliveryAddress { get; set; }

        public CartItemOrderDto CartItemOrderDtos {get; set;}
    
    }
}