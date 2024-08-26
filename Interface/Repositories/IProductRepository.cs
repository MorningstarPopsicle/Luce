namespace Luce.Interface.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetProduct(int id, int sellerId);
        Task<Product> GetProductByProductName(string productName, int sellerId);
        Task<Product> GetProductByProductName(string productName);
        Task<Product> GetProductById(int productId);
        Task<IList<Product>> GetProducts(int sellerId);
        Task<IList<Product>> GetProducts();
        
    }
}