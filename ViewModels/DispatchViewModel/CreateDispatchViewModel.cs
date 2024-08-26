namespace Luce.ViewModels.DispatchViewModel
{
    public class CreateDispatchViewModel
    {
        public int Id {get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }      
        public Role Role { get; set; }
    }
}