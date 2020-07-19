using System.ComponentModel.DataAnnotations;

namespace xsolla_backend_card.Models
{
    public class PaymentByCard
    {
        [Required]
        public CardInfo Card { get; set; }
        
        [Required]
        public string SessionId { get; set; }
    }
}