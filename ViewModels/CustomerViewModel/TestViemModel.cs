using Luce.DTOs;

namespace Luce.ViewModels.CustomerViewModel
{
    public class TestViewModel
    {
        public int Id {get; set;}
        public DateTime Date {get; set;}
        public bool DeliveryIsVerifiedByCustomer {get; set;}
        public bool DeliveryIsVerifiedBySeller {get; set;}
        public AddressDto DeliveryAddress {get; set;}
        public double Distance {get; set;}
        public int RateValue {get; set;}
        public string RefNo {get; set;}
        public int TotalPrice {get; set;}
        public CartItem CartItem {get; set;}
        
    }
}