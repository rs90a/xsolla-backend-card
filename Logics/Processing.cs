using System;
using xsolla_backend_card.Cache;
using xsolla_backend_card.Models;

namespace xsolla_backend_card.Logics
{
    public class Processing
    {
        private readonly ICache cache;

        public Processing(ICache cache)
        {
            this.cache = cache;
        }

        /// <summary>
        /// Создание сессии
        /// </summary>
        /// <param name="paymentInfo">Сведения о платеже</param>
        /// <returns>Id сессии</returns>
        public string CreateSession(PaymentInfo paymentInfo)
        {
            string sessionId = Guid.NewGuid().ToString();
            cache.AddPaymentInfo(sessionId, paymentInfo);
            return sessionId;
        }

        /// <summary>
        /// Выполнение платежа
        /// </summary>
        /// <param name="cardInfo">Сведения о карте</param>
        /// <param name="sessionId">Id сессии</param>
        /// <returns>Сведения о платеже</returns>
        public PaymentInfo BillPayment(PaymentByCard paymentByCard)
        {
            PaymentInfo paymentInfo = cache.GetPaymentInfo(paymentByCard.SessionId);
            bool cardNumIsValid = new Luhn().SimplifiedAlg(paymentByCard.Card);
            if (!cardNumIsValid)
                throw new ArgumentException("Платеж не выполнен. Некорректный номер платежной карты.");
            cache.RemovePaymentInfo(paymentByCard.SessionId);
            return paymentInfo;
        }
    }
}