namespace Luce.Interface.Repositories
{
    public interface IDispatchRepository : IGenericRepository<Dispatch>
    {
        Task<Dispatch> GetDispatch(int id);
        Task<List<Dispatch>> GetDispatches(int sellerId);
        Task<Dispatch> GetDispatch(string email);
       
    }
}