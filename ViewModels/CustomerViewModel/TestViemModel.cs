using Luce.DTOs;

namespace Luce.ViewModels.CustomerViewModel
{
    public class TestViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public AddressDto DeliveryAddress { get; set; }
        public double Distance { get; set; }
        public int RateValue { get; set;}
        public string RefNo {get; set;}
        public double TotalPrice { get; set; }
        // public double TotalPricePerSeller {get; set;}
        public bool DeliveryIsVerifiedByCustomer {get; set;}
        public bool DeliveryIsVerifiedBySeller {get; set;}
        public PaymentDto Payment {get;set;}
        public List<CartItemDto> CartItems { get; set; }

    }

    public class CartItemViewModel
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}