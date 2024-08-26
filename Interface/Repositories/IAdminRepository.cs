namespace Luce.Interface.Repositories
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        Task<Admin> GetAdmin(int id);
   
    }
}