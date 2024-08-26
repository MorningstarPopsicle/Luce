using Microsoft.EntityFrameworkCore;
using Luce.Interface.Repositories;

namespace Luce.Implementation.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(LuceDbContext Context) : base(Context)
        {
            _Context = Context;
        }

        public async Task<Product> GetProduct(int id, int sellerId)
        {
            var product = await _Context.Products.Include(x => x.Seller).ThenInclude(x => x.Address).SingleOrDefaultAsync(c => c.Id == id && c.SellerId == sellerId);
            return product;
        }

        public async Task<Product> GetProductByProductName(string productName, int sellerId)
        {
            var product = await _Context.Products.Include(x => x.Seller).ThenInclude(x => x.Address).SingleOrDefaultAsync(c => c.ProductName == productName && c.SellerId == sellerId);
            return product;
        }

        public async Task<Product> GetProductByProductName(string productName)
        {
            var product = await _Context.Products.Include(x => x.Seller).ThenInclude(x => x.Address).SingleOrDefaultAsync(c => c.ProductName == productName);
            return product;
        }

        public async Task<Product> GetProductById(int productId)
        {
            var product = await _Context.Products.Include(x => x.Seller).ThenInclude(x => x.Address).SingleOrDefaultAsync(c => c.Id == productId);
            return product;
        }

        public async Task<IList<Product>> GetProducts(int sellerId)
        {
            var products = await _Context.Products.Where(a => a.SellerId == sellerId).Include(x => x.Reviews).Include(x => x.Seller).ThenInclude(x => x.Address).ToListAsync();
            return products;
        }

        public async Task<IList<Product>> GetProducts()
        {
            var products = await _Context.Products.Include(x => x.Reviews).Include(x => x.Seller).ThenInclude(x => x.Address).ToListAsync();
            return products;
        }

        
    }
}