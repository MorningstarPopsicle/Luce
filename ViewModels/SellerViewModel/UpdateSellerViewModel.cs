using Luce.DTOs;

namespace Luce.ViewModels.SellerViewModel
{
    public class UpdateSellerViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountNumber { get; set; }
        public string Logo {get; set;}
        public string StoreName {get; set;}
        public AddressDto Address {get; set;}

    }
}