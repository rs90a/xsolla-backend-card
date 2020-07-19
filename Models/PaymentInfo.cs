using System.ComponentModel.DataAnnotations;

namespace xsolla_backend_card.Models
{
    /// <summary>
    /// Сведения о платеже
    /// </summary>
    public class PaymentInfo
    {
        [Required]
        [Range(1, 1000000, ErrorMessage = "Размер платежа должен быть от 1 до 1000000 у.е.")]
        public double Amount { get; set; }
        
        [Required (ErrorMessage = "Назначение платежа не может быть пустым")]
        public string Purpose { get; set; }
    }
}