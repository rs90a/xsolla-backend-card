using System.ComponentModel.DataAnnotations;

namespace xsolla_backend_card.Models
{
    /// <summary>
    /// Сведения о банковской карте
    /// </summary>
    public class CardInfo
    {
        [Required]
        [StringLength(19, MinimumLength = 8, ErrorMessage = "Номер карты должен состоять из 8-19 символов")]
        public string Number { get; set; }
        
        [Required]
        [Range(100, 999, ErrorMessage = "CVC-код должен состоять из трех цифр")]
        public int Cvc { get; set; }
        
        public Date Date { get; set; }
    }
}