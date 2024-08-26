namespace Luce.ViewModels.AdminViewModel
{
    public class CreateAdminViewModel
    {
        public int Id {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password {get; set;}
        public string PhoneNumber { get; set; }       
        public string AccountNumber { get; set; }
        public Role Role { get; set; }
    }
}