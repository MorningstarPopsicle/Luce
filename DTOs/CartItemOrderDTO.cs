namespace Luce.DTOs
{
    public class CartItemOrderDto
    {
        public int Id { get; set; }
        public OrderDto OrderDto { get; set; }
        public CartItemDto ItemDto { get; set; }
    }
}