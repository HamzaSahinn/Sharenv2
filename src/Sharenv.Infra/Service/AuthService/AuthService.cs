using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Sharenv.Application.Interfaces;
using Sharenv.Application.Interfaces.AuthService;
using Sharenv.Application.Models;
using Sharenv.Application.Models.Token;
using Sharenv.Application.Service;
using Sharenv.Application.Validation;
using System.Security.Claims;

namespace Sharenv.Infra.Service.AuthService
{
    public class AuthService : SharenvBaseService, IAuthService
    {
        private IHttpContextAccessor _httpContextAccessor;

        private IUserEntityService _userEntityService;


        private IJwtTokenService _jwtTokenService;

        public AuthService(IUserEntityService userService, IJwtTokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _userEntityService = userService;
            _jwtTokenService = tokenService;
        }

        /// <summary>
        /// Login user via jwt
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Result<LoginResult<GeneratedTokenResponse>> LoginWithJwt(string username, string password)
        {
            return Execute<LoginResult<GeneratedTokenResponse>>(res =>
            {
                ArgumentValidation.ThrowIfNullOrEmpty(username);
                ArgumentValidation.ThrowIfNullOrEmpty(password);

                var loginResult = _userEntityService.IsValidLogin(username, password).ValueOrExceptionWithNullCheck;
                res.Value = new LoginResult<GeneratedTokenResponse>()
                {
                    IsSuccess = loginResult.IsSuccess,
                    User = loginResult.User,
                    FailedReason = loginResult.FailedReason,
                };

                if (!loginResult.IsSuccess)
                {
                    return;
                }

                var claims = CreateClaims(loginResult);
                var tokenResult = _jwtTokenService.GenerateToken(claims);

                res.Value.Data = tokenResult;
            });
        }

        /// <summary>
        /// Login user with auth cookie
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Result<LoginResult> LoginWithAuthCookie(string username, string password)
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

                var claims = CreateClaims(loginResult);
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                if (_httpContextAccessor.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).GetAwaiter().GetResult();
                }
            });
        }

        /// <summary>
        /// Crate claims for auth providers
        /// </summary>
        /// <param name="loginResult"></param>
        /// <returns></returns>
        private List<Claim> CreateClaims(LoginResult loginResult)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, loginResult.User.Id.ToString()),
            };

            return claims;
        }

        /// <summary>
        /// Logout user
        /// </summary>
        /// <returns></returns>
        public void Logout()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).GetAwaiter().GetResult();
            }

            //TODO: Add token blacklist for jwt logout
        }
    }
}
