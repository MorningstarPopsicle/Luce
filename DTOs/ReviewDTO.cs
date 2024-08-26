namespace Luce.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int NoOfStars { get; set; }
        public CustomerDto CustomerDto { get; set; }
        public ProductDto ProductDto { get; set; }


    }
}