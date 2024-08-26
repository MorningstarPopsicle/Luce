namespace Luce.ViewModels.CustomerViewModel
{
    public class CreateCustomerViewModel
    {
        public int Id {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password {get; set;}
        public string PhoneNumber { get; set; }       
        public Role Role { get; set; }
    }
}