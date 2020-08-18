using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using xsolla_backend_card.Auth;

namespace xsolla_backend_card.Middleware
{
    /// <summary>
    /// Middleware для проверки пользовательского контекста.
    /// Выполняется сравнение контекста из токена и кук.
    /// </summary>
    public class TokenContextMiddleware
    {
        private readonly RequestDelegate next;

        public TokenContextMiddleware(RequestDelegate next) =>
            this.next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var tokenFromRequest = await context.GetTokenAsync("access_token");

            if (!string.IsNullOrEmpty(tokenFromRequest))
            {
                try
                {
                    var userCtxFromCookie = context.Request.Cookies["userContext"] ??
                                            throw new AuthenticationException("Пользователь не авторизирован. Доступ запрещен.");

                    var jwtToken = (JwtSecurityToken) new JwtSecurityTokenHandler().ReadToken(tokenFromRequest);
                    var userCtxFromToken =
                        jwtToken.Claims.FirstOrDefault(claim => claim.Type.Equals("userContext"))?.Value ??
                        throw new AuthenticationException("Некорректный формат токена. Доступ запрещен.");

                    if (!JWT.CreateHash(userCtxFromCookie).Equals(userCtxFromToken))
                        throw new AuthenticationException("Похоже Вы не тот за кого себя выдаете. Доступ запрещен.");
                }
                catch (Exception e)
                {
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new BadRequestObjectResult(new
                    {
                        e.Message
                    }), new JsonSerializerSettings()
                    {
                        ContractResolver = new DefaultContractResolver()
                        {
                            NamingStrategy = new CamelCaseNamingStrategy()
                        },
                        Formatting = Formatting.Indented
                    }));
                }
            }

            await next.Invoke(context);
        }
    }
}