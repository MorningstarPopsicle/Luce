namespace Luce
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
        public Address DeliveryAddress { get; set; }
        public DateTime Date { get; set; }
        public int RateValue { get; set; }
        public bool DeliveryIsVerifiedByCustomer { get; set; }
        public bool DeliveryIsVerifiedBySeller { get; set; }
        public double Distance { get; set; }
        public string RefNo { get; set; }
        // public List<Double> Positions {get; set;}
        public List<CartItem> Items { get; set; }
        // public double TotalPrice { get; set; }
        public Order()
        {
            // Positions = new double[4]; 
        }
        public Dictionary<int, double> TotalPricePerSeller()
        {
            var split = new Dictionary<int, List<double>>();
            var result = new Dictionary<int, double>();
            foreach (var item in Items)
            {
                foreach (var id in split.Keys)
                {
                    if (item.Product.SellerId == id)
                    {
                        var price1 = item.Product.Price * item.Quantity;
                        split[id].Add(price1);
                        break;
                    }
                    List<double> prices = new List<double>();
                    var price = item.Product.Price * item.Quantity;
                    prices.Add(price);
                    split.Add(item.Product.SellerId, prices);
                    break;
                }
            }
            foreach (var id in split.Keys)
            {
                double sum = 0;
                foreach (var value in split[id])
                {
                    sum += value;
                }
                result[id] = sum;
            }

            return result;
        }
    }
}