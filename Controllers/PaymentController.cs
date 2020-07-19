using System;
using Microsoft.AspNetCore.Mvc;
using xsolla_backend_card.Cache;
using xsolla_backend_card.Logics;
using xsolla_backend_card.Models;

namespace xsolla_backend_card.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ICache cache;
        private readonly Processing processing;
        
        public PaymentController(ICache cache)
        {
            this.cache = cache;
            processing = new Processing(cache);
        }

        /// <summary>
        /// API-метод для создания новой сессии
        /// </summary>
        /// <param name="paymentInfo">Сведения о платеже</param>
        /// <returns>Id сессии</returns>
        [HttpPost("[action]")]
        public IActionResult Session(PaymentInfo paymentInfo)
        {
            try
            {
                string sessionId = processing.CreateSession(paymentInfo);
                return new OkObjectResult(new {sessionId});
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new 
                {
                    e.Message
                });
            }
        }

        /// <summary>
        /// API-метод для выполнения платежа
        /// </summary>
        /// <param name="cardInfo">Сведения о карте</param>
        /// <param name="sessionId">Id сессии</param>
        /// <returns>Сообщение о статусе платежа</returns>
        [HttpPost("[action]")]
        public IActionResult Begin(PaymentByCard paymentByCard)
        {
            try
            {
                PaymentInfo paymentInfo = processing.BillPayment(paymentByCard);
                return new OkObjectResult(new
                {
                    message = "Платеж выполнен успешно.",
                    paymentInfo
                });
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new
                {
                    e.Message
                });
            }
        }
    }
}