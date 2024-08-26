using Luce.DTOs;
using Luce.Interface.Repositories;
using Luce.Interface.Services;

namespace Luce.Implementation.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISellerRepository _sellerRepository;
        public ProductService(IProductRepository productRepository, IUserRepository userRepository, ISellerRepository sellerRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _sellerRepository = sellerRepository;
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto model, int sellerId)
        {
            var product = await _productRepository.GetProductByProductName(model.ProductName, sellerId);
            if (product != null)
            {
                product.InitialQuantity = model.InitialQuantity;
                product.Quantity = model.InitialQuantity;

                await _productRepository.UpdateAsync(product);
                return new ProductDto()
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    InitialQuantity = product.InitialQuantity,
                    Quantity = product.Quantity,
                    SellerId = product.SellerId
                };
            }

            var newProduct = new Product()
            {
                ProductName = model.ProductName,
                Price = model.Price,
                InitialQuantity = model.InitialQuantity,
                Quantity = model.InitialQuantity,
                ImageUrl = model.ImageUrl,
                SellerId = sellerId,
            };

            var addProduct = await _productRepository.CreateAsync(newProduct);
            if (addProduct == null)
            {
                return null;
            }
            else
            {

                return new ProductDto()
                {
                    ProductName = addProduct.ProductName,
                    Price = addProduct.Price,
                    InitialQuantity = addProduct.InitialQuantity,
                    Quantity = addProduct.InitialQuantity,
                    ImageUrl = addProduct.ImageUrl,
                    SellerId = sellerId,
                };
            }
        }

        public async Task<ProductDto> GetById(int id, int sellerId)
        {
            var product = await _productRepository.GetProduct(id, sellerId);
            if (product == null)
            {
                return null;
            }

            return new ProductDto()
            {
                Price = product.Price,
                ProductName = product.ProductName,
                Id = product.Id,
                InitialQuantity = product.InitialQuantity,
                ImageUrl = product.ImageUrl,
                Quantity = product.Quantity,
                SellerId = product.SellerId,
                SellerDto = new SellerDto
                {
                    Logo = product.Seller.Logo,
                    StoreName = product.Seller.StoreName,
                    AccountNumber = product.Seller.AccountNumber,
                    Id = product.Seller.Id,
                    Address = new AddressDto
                    {
                        HouseNumber = product.Seller.Address.HouseNumber,
                        StreetName = product.Seller.Address.StreetName,
                        LGA = product.Seller.Address.LGA,
                        Town = product.Seller.Address.Town,
                        State = product.Seller.Address.State,
                        Country = product.Seller.Address.Country
                    }
                }
            };
        }
        public async Task<ProductDto> GetByProductName(string productName, int sellerId)
        {
            var product = await _productRepository.GetProductByProductName(productName, sellerId);
            if (product == null)
            {
                return null;
            }

            return new ProductDto()
            {
                Price = product.Price,
                ProductName = product.ProductName,
                Id = product.Id,
                InitialQuantity = product.InitialQuantity,
                ImageUrl = product.ImageUrl,
                Quantity = product.Quantity,
                SellerId = product.SellerId,
                SellerDto = new SellerDto
                {
                    Logo = product.Seller.Logo,
                    StoreName = product.Seller.StoreName,
                    AccountNumber = product.Seller.AccountNumber,
                    Id = product.Seller.Id,
                    Address = new AddressDto
                    {
                        HouseNumber = product.Seller.Address.HouseNumber,
                        StreetName = product.Seller.Address.StreetName,
                        LGA = product.Seller.Address.LGA,
                        Town = product.Seller.Address.Town,
                        State = product.Seller.Address.State,
                        Country = product.Seller.Address.Country
                    }
                }
            };
        }

        public async Task<ProductDto> GetProductByProductName(string productName)
        {
            var product = await _productRepository.GetProductByProductName(productName);
            if (product == null)
            {
                return null;
            }

            return new ProductDto()
            {
                Price = product.Price,
                ProductName = product.ProductName,
                Id = product.Id,
                InitialQuantity = product.InitialQuantity,
                ImageUrl = product.ImageUrl,
                Quantity = product.Quantity,
                SellerId = product.SellerId,
                SellerDto = new SellerDto
                {
                    Logo = product.Seller.Logo,
                    StoreName = product.Seller.StoreName,
                    AccountNumber = product.Seller.AccountNumber,
                    Id = product.Seller.Id,
                    Address = new AddressDto
                    {
                        HouseNumber = product.Seller.Address.HouseNumber,
                        StreetName = product.Seller.Address.StreetName,
                        LGA = product.Seller.Address.LGA,
                        Town = product.Seller.Address.Town,
                        State = product.Seller.Address.State,
                        Country = product.Seller.Address.Country
                    }
                }
            };
        }


        public async Task<ProductDto> GetProductById(int productId)
        {
            var product = await _productRepository.GetProductById(productId);
            if (product == null)
            {
                return null;
            }

            return new ProductDto()
            {
                Price = product.Price,
                ProductName = product.ProductName,
                Id = product.Id,
                InitialQuantity = product.InitialQuantity,
                ImageUrl = product.ImageUrl,
                Quantity = product.Quantity,
                SellerId = product.SellerId,
                SellerDto = new SellerDto
                {
                    Logo = product.Seller.Logo,
                    StoreName = product.Seller.StoreName,
                    AccountNumber = product.Seller.AccountNumber,
                    Id = product.Seller.Id,
                    Address = new AddressDto
                    {
                        HouseNumber = product.Seller.Address.HouseNumber,
                        StreetName = product.Seller.Address.StreetName,
                        LGA = product.Seller.Address.LGA,
                        Town = product.Seller.Address.Town,
                        State = product.Seller.Address.State,
                        Country = product.Seller.Address.Country
                    }
                }

            };
        }

        public async Task<bool> UpdateProduct(ProductDto updatedProduct, int productId, int sellerId)
        {
            var product = await _productRepository.GetProduct(productId, sellerId);
            if (product == null)
            {
                return false;
            }
            product.Price = product.Price != 0 ? updatedProduct.Price : product.Price;
            product.Quantity = updatedProduct.Quantity;
            product.InitialQuantity = product.Quantity;
            product.ProductName = product.ProductName != null ? updatedProduct.ProductName : product.ProductName;
            product.ImageUrl = product.ImageUrl != null ? updatedProduct.ImageUrl : product.ImageUrl;
            await _productRepository.UpdateAsync(product);
            return true;
        }

        public async Task<List<ProductDto>> GetBySellerId(int id)
        {
            var products = await _productRepository.GetProducts(id);
            if (products == null)
            {
                return null;
            }
            return products.Select(x => new ProductDto
            {
                Id = x.Id,
                Price = x.Price,
                ProductName = x.ProductName,
                InitialQuantity = x.InitialQuantity,
                Quantity = x.Quantity,
                ImageUrl = x.ImageUrl,
            }).ToList();
        }

        public async Task<List<ProductDto>> GetAllProducts()
        {
            var products = await _productRepository.GetProducts();
            if (products == null)
            {
                return null;
            }
            return products.Select(x => new ProductDto
            {

                Id = x.Id,
                Price = x.Price,
                ProductName = x.ProductName,
                InitialQuantity = x.InitialQuantity,
                Quantity = x.Quantity,
                ImageUrl = x.ImageUrl,
            }).ToList();

        }

    }
}
