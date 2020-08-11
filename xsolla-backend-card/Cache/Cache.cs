using System;
using Microsoft.Extensions.Caching.Memory;
using xsolla_backend_card.Models;

namespace xsolla_backend_card.Cache
{
    /// <summary>
    /// Кэширование
    /// </summary>
    public class Cache : ICache
    {
        private readonly IMemoryCache cache;

        public Cache(IMemoryCache cache)
        {
            this.cache = cache;
        }

        /// <summary>
        /// Получение сведений о платежной сессии
        /// </summary>
        /// <param name="sessionId">Id сессии</param>
        /// <returns>Сведения о платеже</returns>
        public PaymentInfo GetPaymentInfo(string sessionId)
        {
            PaymentInfo paymentInfo = null;
            if (cache.TryGetValue($"{CacheKeys.Session}_{sessionId}", out paymentInfo))
                return paymentInfo;
            throw new ArgumentException($"Время сессии {sessionId} истекло.");
        }

        /// <summary>
        /// Добавление сведений о платежной сессии в кэш. Время сессии - 2 мин.
        /// </summary>
        /// <param name="sessionId">Id сессии</param>
        /// <param name="paymentInfo">Сведения о платеже</param>
        public void AddPaymentInfo(string sessionId, PaymentInfo paymentInfo)
        {
            cache.Set($"{CacheKeys.Session}_{sessionId}", paymentInfo, new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
            });
        }

        /// <summary>
        /// Удаление сведений о сессии из кэша
        /// </summary>
        /// <param name="sessionId">Id сессии</param>
        public void RemovePaymentInfo(string sessionId)
        {
            cache.Remove($"{CacheKeys.Session}_{sessionId}");
        }
    }
}