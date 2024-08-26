namespace Luce.DTOs
{
    public class CartItemDto
    {
        public int Id {get; set;}
        public int Quantity {get; set;}
        // public int TotalPrice {get; set;}
        public int? OrderId {get; set;}
        public int ProductId {get; set;}
        public int CustomerId {get; set;}
        public ProductDto ProductDto {get; set;}
        public CustomerDto CustomerDto {get; set;}
        public bool IsCheckedOut {get; set;}
    }

    
}