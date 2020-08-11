using System.ComponentModel.DataAnnotations;

namespace xsolla_backend_card.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        [Required]
        public string Login { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}