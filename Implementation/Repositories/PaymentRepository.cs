using Microsoft.EntityFrameworkCore;
using Luce.Interface.Repositories;

namespace Luce.Implementation.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(LuceDbContext Context) : base(Context)
        {
            _Context = Context;
        }

       public async Task<Payment> GetPayment(int id)
        {
            var payment = await _Context.Payments.Include(x => x.Customer).Include(x => x.Seller).SingleOrDefaultAsync(c => c.Id == id);
            return payment;}

        public async Task<List<Payment>> GetAllPayments()
        {
            var payments =  await _Context.Payments.Include(p => p.Seller).Include(p => p.Customer).ToListAsync();
            return payments;
        }       
    }
}