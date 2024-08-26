using Luce.DTOs;
namespace Luce.Interface.Services
{
    public interface IPaymentService
    {
        Task<bool> MakePayment(PaymentDto model, int customerId, int sellerId);
        // Task<PaymentsResponseModel> GetPaymentById(int paymentId);
    }
}