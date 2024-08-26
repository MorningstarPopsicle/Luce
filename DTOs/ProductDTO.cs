namespace Luce.DTOs
{
    public class ProductDto
    {
        public int Id {get; set;}
        public double Price {get; set;}
        public string ImageUrl {get; set;}
        public string ProductName {get; set;}
        public int Quantity {get; set;}
        public int InitialQuantity {get; set;}
        public int SellerId {get; set;}
        public SellerDto SellerDto {get; set;}
        public List<ReviewDto> ReviewDtos {get; set;}
    }
}