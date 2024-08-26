// using Luce.DTOs;
// using Luce.Interface.Repositories;
// using Luce.Interface.Services;

// namespace Luce.Implementation.Service
// {
//     public class OrderService : IOrderService
//     {
//         private readonly IOrderRepository _orderRepository;
//         private readonly ICartItemRepository _cartItemRepository;
//         private readonly IPaymentRepository _paymentRepository;
//         public OrderService(IPaymentRepository paymentRepository, IOrderRepository orderRepository, ICartItemRepository cartItemRepository)
//         {
//             _paymentRepository = paymentRepository;
//             _orderRepository = orderRepository;
//             _cartItemRepository = cartItemRepository;

//         }

//         public async Task<Order> CreateOrder(OrderDto model, int cartItemId)
//         {
//             var cartItem = await _cartItemRepository.GetAsync(cartItemId);

//             var order = new Order
//             {
//                 DeliveryAddress = new Address
//                 {
//                     HouseNumber = model.DeliveryAddress.HouseNumber,
//                     StreetName = model.DeliveryAddress.StreetName,
//                     LGA = model.DeliveryAddress.LGA,
//                     Town = model.DeliveryAddress.Town,
//                     State = model.DeliveryAddress.State,
//                     Country = model.DeliveryAddress.Country,
//                 },
//                 DeliveryIsVerifiedByCustomer = false,
//                 DeliveryIsVerifiedBySeller = false,
//                 RefNo = $"ORD{Guid.NewGuid().ToString().Replace("-", " ").Substring(0, 5).ToUpper()}",
//                 TotalPrice = model.TotalPrice,
//                 // Positions = model.Positions,
//                 RateValue = model.RateValue,
//                 Distance = model.Distance,
//                 CustomerId = cartItem.CustomerId,
//                 SellerId = cartItem.Product.SellerId,
//                 Payment = new Payment
//                 {
//                     Id = model.PaymentDto.Id,
//                     Date = DateTime.Now,
//                     Amount = model.PaymentDto.Amount,
//                     Status = model.PaymentDto.Status
//                 }
//             };

//             var result = await _orderRepository.CreateAsync(order);

//             return new OrderDto()
//             {
//                 Id = result.Id,
//                 DeliveryAddress = new AddressDto()
//                 {
//                     HouseNumber = result.DeliveryAddress.HouseNumber,
//                     StreetName = result.DeliveryAddress.StreetName,
//                     LGA = result.DeliveryAddress.LGA,
//                     Town = result.DeliveryAddress.Town,
//                     State = result.DeliveryAddress.State,
//                     Country = result.DeliveryAddress.Country,
//                 },
//                 DeliveryIsVerifiedByCustomer = result.DeliveryIsVerifiedByCustomer,
//                 DeliveryIsVerifiedBySeller = result.DeliveryIsVerifiedBySeller,
//                 RefNo = result.RefNo,
//                 TotalPrice = result.TotalPrice,
//                 RateValue = result.RateValue,
//                 Distance = result.Distance,
//                 PaymentId = 
               
//             };
//         }

//         public async Task<OrderResponseModel> GetByRefNo(string refNo)
//         {
//             var order = await _orderRepository.GetOrder(refNo);
//             if (order == null)
//             {
//                 return new OrderResponseModel
//                 {
//                     Message = "Order not found",
//                     Success = false,
//                 };
//             }

//             return new OrderResponseModel
//             {
//                 Message = "Order Retrived Successfully",
//                 Success = true,
//                 Data = new OrderDto
//                 {
//                     CustomerDto = new CustomerDto
//                     {
//                         Id = order.Customer.Id,
//                         // UserDto = new UserDto
//                         // {
//                         //     Id = order.Seller.User.Id,
//                         //     FirstName = seller.User.FirstName,
//                         //     LastName = seller.User.LastName,
//                         //     Email = seller.User.Email,
//                         //     PhoneNumber = seller.User.PhoneNumber,
//                         //     Role = seller.User.Role,
//                         // },
//                     },
//                     SellerDto = new SellerDto
//                     {
//                         Logo = order.Seller.Logo,
//                         StoreName = order.Seller.StoreName,
//                         AccountNumber = order.Seller.AccountNumber,
//                         Id = order.Seller.Id,
//                         IsVerified = order.Seller.IsVerified,
//                         Address = new AddressDto
//                         {
//                             HouseNumber = order.Seller.Address.HouseNumber,
//                             StreetName = order.Seller.Address.StreetName,
//                             LGA = order.Seller.Address.LGA,
//                             Town = order.Seller.Address.Town,
//                             State = order.Seller.Address.State,
//                             Country = order.Seller.Address.Country
//                         }
//                     },
//                     Distance = order.Distance,
//                     RateValue = order.RateValue,
//                     DeliveryIsVerifiedByCustomer = order.DeliveryIsVerifiedByCustomer,
//                     DeliveryIsVerifiedBySeller = order.DeliveryIsVerifiedBySeller,
//                     RefNo = order.RefNo,
//                     TotalPrice = order.TotalPrice,
//                     DeliveryAddress = new AddressDto
//                         {
//                             HouseNumber = order.DeliveryAddress.HouseNumber,
//                             StreetName = order.DeliveryAddress.StreetName,
//                             LGA = order.DeliveryAddress.LGA,
//                             Town = order.DeliveryAddress.Town,
//                             State = order.DeliveryAddress.State,
//                             Country = order.DeliveryAddress.Country
//                         },
//                     PaymentDto = new PaymentDto
//                     {
//                         Id = order.Payment.Id,
//                     },
//                     Id = order.Id,
//                 }
//             };
//         }
//         public async Task<OrdersResponseModel> GetByCustomerId(int id)
//         {
//             var orders = await _orderRepository.GetOrders(id);
//             if (orders == null)
//             {
//                 return new OrdersResponseModel
//                 {
//                     Message = "Order(s) not found",
//                     Success = false,
//                 };
//             }

//             return new OrdersResponseModel
//             {
//                 Message = "Orders Retrived Successfully",
//                 Success = true,
//                 Data = orders.Select(order => new OrderDto()
//                 {
//                     CustomerDto = new CustomerDto
//                     {
//                         Id = order.Customer.Id,
//                         // UserDto = new UserDto
//                         // {
//                         //     Id = order.Seller.User.Id,
//                         //     FirstName = seller.User.FirstName,
//                         //     LastName = seller.User.LastName,
//                         //     Email = seller.User.Email,
//                         //     PhoneNumber = seller.User.PhoneNumber,
//                         //     Role = seller.User.Role,
//                         // },
//                     },
//                     SellerDto = new SellerDto
//                     {
//                         Logo = order.Seller.Logo,
//                         StoreName = order.Seller.StoreName,
//                         AccountNumber = order.Seller.AccountNumber,
//                         Id = order.Seller.Id,
//                         IsVerified = order.Seller.IsVerified,
//                         Address = new AddressDto
//                         {
//                             HouseNumber = order.Seller.Address.HouseNumber,
//                             StreetName = order.Seller.Address.StreetName,
//                             LGA = order.Seller.Address.LGA,
//                             Town = order.Seller.Address.Town,
//                             State = order.Seller.Address.State,
//                             Country = order.Seller.Address.Country
//                         }
//                     },
//                     Distance = order.Distance,
//                     RateValue = order.RateValue,
//                     DeliveryIsVerifiedByCustomer = order.DeliveryIsVerifiedByCustomer,
//                     DeliveryIsVerifiedBySeller = order.DeliveryIsVerifiedBySeller,
//                     RefNo = order.RefNo,
//                     TotalPrice = order.TotalPrice,
//                     DeliveryAddress = new AddressDto
//                         {
//                             HouseNumber = order.DeliveryAddress.HouseNumber,
//                             StreetName = order.DeliveryAddress.StreetName,
//                             LGA = order.DeliveryAddress.LGA,
//                             Town = order.DeliveryAddress.Town,
//                             State = order.DeliveryAddress.State,
//                             Country = order.DeliveryAddress.Country
//                         },
//                     PaymentDto = new PaymentDto
//                         {
//                             Id = order.Payment.Id,
//                         },
//                     Id = order.Id,
//                 }).ToList()
//             };
//         }


//     }
// }