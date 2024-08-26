using Luce;
using Luce.DTOs;

namespace Luce.Interface.Services
{
    public interface IProductService
    {   
        Task<ProductDto> CreateProductAsync(ProductDto product , int id);
        Task<ProductDto> GetById(int id, int sellerId);
        Task<ProductDto> GetByProductName(string productName, int sellerId);
        Task<ProductDto> GetProductByProductName(string productName);
        Task<ProductDto> GetProductById(int productId);
        Task<List<ProductDto>> GetBySellerId(int id);
        Task<List<ProductDto>> GetAllProducts();
        Task<bool> UpdateProduct(ProductDto updatedProduct, int productId, int sellerId);   
    }
}