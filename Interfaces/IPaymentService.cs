using xsolla_backend_card.Models;

namespace xsolla_backend_card.Interfaces
{
    public interface IPaymentService
    {
        public string CreateSession(PaymentInfo paymentInfo);
        public PaymentInfo BillPayment(PaymentByCard paymentByCard);
    }
}