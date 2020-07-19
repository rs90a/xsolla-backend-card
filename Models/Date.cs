using System.ComponentModel.DataAnnotations;
using xsolla_backend_card.Validators;

namespace xsolla_backend_card.Models
{
    /// <summary>
    /// Срок действия карты
    /// </summary>
    public class Date
    {
        [Required]
        [Range(1, 12)]
        public int Month { get; set; }
        
        [Required]
        [Range(2000, 9999)]
        [CardExpiration]
        public int Year { get; set; }
    }
}