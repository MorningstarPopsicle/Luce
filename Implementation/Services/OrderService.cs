using Luce.DTOs;
using Luce.Interface.Repositories;
using Luce.Interface.Services;
using Luce.ViewModels.CustomerViewModel;

namespace Luce.Implementation.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPaymentRepository _paymentRepository;
        public OrderService(IPaymentRepository paymentRepository, IOrderRepository orderRepository, ICartItemRepository cartItemRepository, IProductRepository productRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;

        }

        public async Task<OrderDto> CreateOrder(OrderDto model)
        {
            var order = await _orderRepository.GetAsync(model.Id);
            if (order != null)
            {
                return null;
            }
            

            var payment = new Payment()
            {
                Date = DateTime.Now,
                Amount = model.PaymentDto.Amount,
                Status = PaymentStatus.Completed,
            };
            var addPayment = await _paymentRepository.CreateAsync(payment);
            var newOrder = new Order
            {
                DeliveryAddress = new Address
                {
                    HouseNumber = model.DeliveryAddress.HouseNumber,
                    StreetName = model.DeliveryAddress.StreetName,
                    LGA = model.DeliveryAddress.LGA,
                    Town = model.DeliveryAddress.Town,
                    State = model.DeliveryAddress.State,
                    Country = model.DeliveryAddress.Country,
                },
                DeliveryIsVerifiedByCustomer = false,
                DeliveryIsVerifiedBySeller = false,
                RefNo = $"ORD{Guid.NewGuid().ToString().Replace("-", " ").Substring(0, 5).ToUpper()}",
                Date = DateTime.Now,
                RateValue = model.RateValue,
                Distance = model.Distance,
                Payment = new Payment
                {
                    Id = model.PaymentDto.Id,
                    Amount = model.PaymentDto.Amount
                },
                Customer = new Customer
                {
                    Id = model.CustomerDto.Id
                },
                Items = model.Items.Select(item => new CartItem
                {
                    Product = new Product
                    {
                        SellerId = item.ProductDto.SellerId,
                        Price = item.ProductDto.Price,
                        ProductName = item.ProductDto.ProductName
                    },
                    Quantity = item.Quantity
                }).ToList(),

            };

            var result = await _orderRepository.CreateAsync(order);
            if (result == null)
            {
                return null;
            }
            else
            {
                foreach (var item in result.Items)
                {

                    item.IsCheckedOut = true;
                    item.OrderId = result.Id;
                    await _cartItemRepository.UpdateAsync(item);
                    var product = await _productRepository.GetAsync(item.ProductId);
                    product.Quantity -= item.Quantity;
                    await _productRepository.UpdateAsync(product);
                }

                return new OrderDto()
                {
                    Id = result.Id,
                    DeliveryIsVerifiedByCustomer = result.DeliveryIsVerifiedByCustomer,
                    DeliveryIsVerifiedBySeller = result.DeliveryIsVerifiedBySeller,
                    RefNo = result.RefNo,
                    Date = result.Date,
                    RateValue = result.RateValue,
                    Distance = result.Distance,
                    DeliveryAddress = new AddressDto()
                    {
                        HouseNumber = result.DeliveryAddress.HouseNumber,
                        StreetName = result.DeliveryAddress.StreetName,
                        LGA = result.DeliveryAddress.LGA,
                        Town = result.DeliveryAddress.Town,
                        State = result.DeliveryAddress.State,
                        Country = result.DeliveryAddress.Country
                    },
                    PaymentDto = new PaymentDto()
                    {
                        Id = result.Payment.Id,
                        Date = result.Payment.Date,
                        Amount = result.Payment.Amount,
                        Status = result.Payment.Status
                    },
                    CustomerDto = new CustomerDto()
                    {
                        Id = result.Customer.Id,
                        UserDto = new UserDto()
                        {
                            Id = result.Customer.User.Id,
                            FirstName = result.Customer.User.FirstName,
                            LastName = result.Customer.User.LastName,
                            Email = result.Customer.User.Email,
                            PhoneNumber = result.Customer.User.PhoneNumber,
                        }
                    },
                    Items = result.Items.Select(x => new CartItemDto
                    {
                        ProductDto = new ProductDto
                        {
                            SellerId = x.Product.Seller.Id,
                            Price = x.Product.Price,
                            ProductName = x.Product.ProductName
                        },
                        Quantity = x.Quantity
                    }).ToList()
                };
            }

        }
        public async Task<OrderDto> GetByRefNo(string refNo)
        {
            var order = await _orderRepository.GetOrder(refNo);
            if (order == null)
            {
                return null;
            }

            return new OrderDto()
            {
                CustomerDto = new CustomerDto
                {
                    Id = order.Customer.Id,
                    UserDto = new UserDto
                    {
                        Id = order.Customer.User.Id,
                        FirstName = order.Customer.User.FirstName,
                        LastName = order.Customer.User.LastName,
                        Email = order.Customer.User.Email,
                        PhoneNumber = order.Customer.User.PhoneNumber,
                        Role = order.Customer.User.Role,
                    },
                },
                Distance = order.Distance,
                RateValue = order.RateValue,
                DeliveryIsVerifiedByCustomer = order.DeliveryIsVerifiedByCustomer,
                DeliveryIsVerifiedBySeller = order.DeliveryIsVerifiedBySeller,
                RefNo = order.RefNo,
                Date = order.Date,
                DeliveryAddress = new AddressDto
                {
                    HouseNumber = order.DeliveryAddress.HouseNumber,
                    StreetName = order.DeliveryAddress.StreetName,
                    LGA = order.DeliveryAddress.LGA,
                    Town = order.DeliveryAddress.Town,
                    State = order.DeliveryAddress.State,
                    Country = order.DeliveryAddress.Country
                },
                PaymentDto = new PaymentDto
                {
                    Id = order.Payment.Id,
                    Date = order.Payment.Date,
                    Status = order.Payment.Status,
                    Amount = order.Payment.Amount
                },
                Id = order.Id,
                Items = order.Items.Select(x => new CartItemDto
                {
                    ProductDto = new ProductDto
                    {
                        SellerId = x.Product.Seller.Id,
                        Price = x.Product.Price,
                        ProductName = x.Product.ProductName
                    },
                    Quantity = x.Quantity
                }).ToList()
            };
        }

        public async Task<List<OrderDto>> GetByCustomerId(int id)
        {
            var orders = await _orderRepository.GetOrders(id);
            if (orders == null)
            {
                return null;
            }

            return orders.Select(order => new OrderDto
            {
                CustomerDto = new CustomerDto
                {
                    Id = order.Customer.Id,
                    UserDto = new UserDto
                    {
                        Id = order.Customer.User.Id,
                        FirstName = order.Customer.User.FirstName,
                        LastName = order.Customer.User.LastName,
                        Email = order.Customer.User.Email,
                        PhoneNumber = order.Customer.User.PhoneNumber,
                        Role = order.Customer.User.Role,
                    },
                },

                Distance = order.Distance,
                RateValue = order.RateValue,
                DeliveryIsVerifiedByCustomer = order.DeliveryIsVerifiedByCustomer,
                DeliveryIsVerifiedBySeller = order.DeliveryIsVerifiedBySeller,
                RefNo = order.RefNo,
                Date = order.Date,
                DeliveryAddress = new AddressDto
                {
                    HouseNumber = order.DeliveryAddress.HouseNumber,
                    StreetName = order.DeliveryAddress.StreetName,
                    LGA = order.DeliveryAddress.LGA,
                    Town = order.DeliveryAddress.Town,
                    State = order.DeliveryAddress.State,
                    Country = order.DeliveryAddress.Country
                },

                PaymentDto = new PaymentDto
                {
                    Id = order.Payment.Id,
                    Status = order.Payment.Status,
                    Amount = order.Payment.Amount,
                    Date = order.Payment.Date
                },
                Id = order.Id,
                Items = order.Items.Select(x => new CartItemDto
                {
                    ProductDto = new ProductDto
                    {
                        SellerId = x.Product.Seller.Id,
                        Price = x.Product.Price,
                        ProductName = x.Product.ProductName
                    },
                    Quantity = x.Quantity
                }).ToList()

            }).ToList();
        }

        // public  Dictionary<int, double> GetTotalPricesBySeller(OrderDto order)
        // {
            
        //     var newOrder = new Order()
        //     {
        //         Items = order.Items.Select(x => new CartItem
        //         {
        //             Product = new Product
        //             {
        //                 SellerId = x.ProductDto.SellerDto.Id,
        //                 ProductName = x.ProductDto.ProductName,
        //                 Price = x.ProductDto.Price
        //             },
        //             Quantity = x.Quantity
        //         }).ToList()
        //     };
        //     return newOrder.TotalPricePerSeller();
        // }
        


    }
}