using System.Linq.Expressions;
namespace Luce.Interface.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        // Task<IEnumerable<User>> GetAllUser();
        // Task<IEnumerable<User>> GetAllUser(Expression<Func<User, bool>> expression, bool AsNoTracking = true, CancellationToken cancellationToken = default);
     
        // Task<User> GetUser(string Id);
        // Task<User> GetUser(Expression<Func<User, bool>> expression, bool AsNoTracking = true, CancellationToken cancellationToken = default);
        // Task<User> GetUserAsync(string email);
        // Task<User> GetUserByEmailAsync(string email);
        // Task<string> GetUserNameById(string Id);
        // Task<List<UserDto>> LoadUsersAsync(string filter, int page, int limit);
    }
}