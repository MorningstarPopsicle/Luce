namespace Luce.Interface.Repositories
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<Payment> GetPayment(int id);
        Task<List<Payment>> GetAllPayments();
   
    }
}