using Microsoft.EntityFrameworkCore;
using Luce.Interface.Repositories;

namespace Luce.Implementation.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {

        public CustomerRepository(LuceDbContext Context): base(Context)
        {
            _Context = Context;
        }

        
        // public async Task<BaseResponse> ExistsByEmailAsync(string Email, string passWord)
        // {
        //     var customer = await _Context.Customers.FirstOrDefaultAsync(c => c.User.Email == Email && c.User.Password == passWord);
        //     if (customer == null)
        //     {
        //         return new BaseResponse()
        //         {
        //             Message = "Customer Not Found",
        //             Success = false,
        //         };
        //     }
        //     return new BaseResponse()
        //     {
        //         Message = "Customer Found",
        //         Success = true,
        //     };
        // }
        
        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await _Context.Customers.Include(x => x.User).SingleOrDefaultAsync(c => c.UserId == id);
            return customer;
        }
    }
}

