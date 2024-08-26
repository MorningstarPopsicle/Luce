using Luce.DTOs;
namespace Luce.Interface.Services
{
    public interface IReviewService
    {   
        Task<ReviewDto> CreateReviewAsync(ReviewDto model,  int productId, int customerId);
        // Task<ReviewResponseModel> GetById(int id);
        // Task<ReviewsResponseModel> GetByProductId(int id);

    }
}