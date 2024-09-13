using Luce.DTOs;
using Luce.Interface.Repositories;
using Luce.Interface.Services;

namespace Luce.Implementation.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICustomerRepository _customerRepository;

        public PaymentService(IPaymentRepository paymentRepository, ICustomerRepository customerRepository)
        {
            _paymentRepository = paymentRepository;
            _customerRepository = customerRepository;

        }

        // public async Task<bool> MakePayment(PaymentDto model, int customerId, int sellerId)
        // {
        //     var payment = await _paymentRepository.GetAsync(payment => payment.Id == model.Id);
        //     if (payment != null)
        //     {
        //         return false;
        //     }

        //     var newPayment = new Payment()
        //     {
        //         Date = DateTime.Now,
        //         Amount = model.Amount,
        //         Status = PaymentStatus.Completed,
        //         SenderId = customerId,
        //         ReceiverId = sellerId, 
        //     };
        //     var addPayment = await _paymentRepository.CreateAsync(newPayment);
        //     return true;
        // }
    }
}