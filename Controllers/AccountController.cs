using System;
using Microsoft.AspNetCore.Mvc;
using xsolla_backend_card.Interfaces;
using xsolla_backend_card.Models;

namespace xsolla_backend_card.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("[action]")]
        public IActionResult Token([FromBody]User user)
        {
            try
            {
                return new OkObjectResult(new
                {
                    token = accountService.GetToken(user)
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