namespace Luce.DTOs
{
    public class SellerDto
    {
        public int Id {get; set;}
        public UserDto UserDto {get; set;}
        public bool IsVerified {get; set;}
        public string AccountNumber {get; set;}
        public string Logo {get; set;}
        public string StoreName {get; set;}
        public AddressDto Address { get; set; }

        public List<ProductDto> ProductDtos {get; set;}
        public List<OrderDto> OrderDtos {get; set;}
        public List<DispatchDto> DispatchDtos {get; set;}
         public List<PaymentDto> PaymentDtos {get; set;}
    }
}