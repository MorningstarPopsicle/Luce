namespace Luce
{
    public class CartItem : BaseEntity
    {
        public int ProductId {get; set;}
        public Product Product {get; set;}
        public int CustomerId {get; set;}
        public Customer Customer {get; set;}
        public int Quantity {get; set;}
        public int? OrderId {get; set;} 
        public Order? Order {get; set;} 
        public bool IsCheckedOut {get; set;}  
    }
}