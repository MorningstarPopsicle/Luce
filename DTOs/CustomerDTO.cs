namespace Luce.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public UserDto UserDto {get; set;}
        public List<CartItemDto> CartItemDtos {get; set;}
        public List<OrderDto> OrderDtos {get; set;}
        public List<ReviewDto> ReviewDtos {get; set;}
        public List<PaymentDto> PaymentDtos {get; set;}
       
    }
}