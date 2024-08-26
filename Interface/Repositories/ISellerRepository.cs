namespace Luce.Interface.Repositories
{
    public interface ISellerRepository : IGenericRepository<Seller>
    {
        Task<Seller> GetSeller(int id);
        Task<IList<Seller>> GetSellers();
    }
}