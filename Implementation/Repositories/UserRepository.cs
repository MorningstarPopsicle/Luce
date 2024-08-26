using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Luce.Interface.Repositories;

namespace Luce.Implementation.Repositories
{

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly LuceDbContext _dbcontext;

        public UserRepository(LuceDbContext context) : base(context)
        {
            _dbcontext = context;
        }

    }
}