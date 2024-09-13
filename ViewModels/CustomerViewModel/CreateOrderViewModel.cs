using Luce.DTOs;

namespace Luce.ViewModels.CustomerViewModel
{
    public class CreateOrderViewModel
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
        
    }

   
    public class OrderCartItemViewModel
    {
        public CartItemDto CartItem { get; set; }
        public CreateOrderViewModel Order { get; set; }
    }

}