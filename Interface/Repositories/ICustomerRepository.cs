namespace Luce.Interface.Repositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer> GetCustomer(int id);
        
        
        
    }
}