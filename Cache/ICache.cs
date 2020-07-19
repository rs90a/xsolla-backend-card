using xsolla_backend_card.Models;

namespace xsolla_backend_card.Cache
{
    public interface ICache
    {
        public PaymentInfo GetPaymentInfo(string sessionId);
        public void AddPaymentInfo(string sessionId, PaymentInfo paymentInfo);
        public void RemovePaymentInfo(string sessionId);
    }
}