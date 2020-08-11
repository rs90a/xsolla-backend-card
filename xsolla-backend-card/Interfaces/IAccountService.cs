using xsolla_backend_card.Models;

namespace xsolla_backend_card.Interfaces
{
    public interface IAccountService
    {
        public string GetToken(User user);
    }
}