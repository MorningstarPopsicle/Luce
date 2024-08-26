namespace Luce.Interface.Repositories
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<Review> GetReview( int productId);
        Task<IList<Review>> GetReviews(int productId);
    }
}