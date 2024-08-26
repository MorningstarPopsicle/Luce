using Luce.DTOs;

namespace Luce.ViewModels.CustomerViewModel
{
    public class CreateCartItemViewModel
    {
        public int Id {get; set;}
        public int Quantity { get; set; }
        // public int TotalPrice {get; set;}
        public bool IsCheckedOut {get; set;}
        public int? OrderId {get; set;}
    }

   
    public class ProductCartItemViewModel
    {
        public ProductDto Product { get; set; }
        public CreateCartItemViewModel CartItem { get; set; }
    }

}