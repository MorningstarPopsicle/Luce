namespace Luce
{
    public class Address : BaseEntity
    {
        public string HouseNumber {get; set;}
        public string StreetName {get; set;}
        public string LGA {get; set;}
        public string Town {get; set;}
        public string State {get; set;}
        public string Country {get; set;}  
        public int SellerId {get; set;}
        public Seller Seller {get; set;}
    }
}