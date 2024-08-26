namespace Luce
{
    public class Admin : BaseEntity
    {
        public int UserId {get; set;}
        public User User {get; set;}
        public string AccountNumber {get; set;}
        public List<Payment> Payments {get; set;}

    }
}