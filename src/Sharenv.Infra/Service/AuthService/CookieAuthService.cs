using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Sharenv.Application.Interfaces;
using Sharenv.Application.Interfaces.AuthService;
using Sharenv.Application.Models;
using Sharenv.Application.Service;
using Sharenv.Application.Validation;
using System.Security.Claims;

namespace Sharenv.Infra.Service.AuthService
{
    public class CookieAuthService : SharenvBaseService, ICookieAuthService
    {
        private IHttpContextAccessor _httpContextAccessor;

        private IUserEntityService _userEntityService;

        public CookieAuthService(IUserEntityService userService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userEntityService = userService;
        }

        /// <summary>
        /// Login user via cookie
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result<LoginResult> Login(string username, string password)
        {
            return Execute<LoginResult>(res =>
            {
                ArgumentValidation.ThrowIfNullOrEmpty(username);
                ArgumentValidation.ThrowIfNullOrEmpty(password);

                var loginResult = _userEntityService.IsValidLogin(username, password).ValueOrExceptionWithNullCheck;
                res.Value = loginResult;
                
                if (!loginResult.IsSuccess)
                {
                    return;
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, loginResult.User.Id.ToString()),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                if (_httpContextAccessor.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.SignInAsync(principal).GetAwaiter().GetResult();
                }
            });
        }

        /// <summary>
        /// Logout user
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public void Logout()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                _httpContextAccessor.HttpContext.SignOutAsync().GetAwaiter().GetResult();
            }
        }
    }
}
