using Microsoft.EntityFrameworkCore;
using Luce.Interface.Repositories;

namespace Luce.Implementation.Repositories
{
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {

        public ReviewRepository(LuceDbContext Context) : base(Context)
        {
            _Context = Context;
        }

        public async Task<Review> GetReview( int productId)
        {
            var review = await _Context.Reviews.Include(x => x.Customer).Include(x => x.Product).SingleOrDefaultAsync(c => c.Id == productId && c.ProductId == productId);
            return review;
        }

        public async Task<IList<Review>> GetReviews(int productId)
        {
            var reviews = await _Context.Reviews.Where(x => x.ProductId == productId).Include(x => x.Customer).Include(a => a.Product).ToListAsync();
            return reviews;
        }
    }
}
