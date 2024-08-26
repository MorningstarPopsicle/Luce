using Microsoft.EntityFrameworkCore;
using Luce.Interface.Repositories;

namespace Luce.Implementation.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {

        public AdminRepository(LuceDbContext Context) : base(Context)
        {
            _Context = Context;
        } 
        public async Task<Admin> GetAdmin(int id)
        {
            var admin = await _Context.Admins.Include(x => x.User).SingleOrDefaultAsync(c => c.UserId == id);
            return admin;
        }
    }
}
