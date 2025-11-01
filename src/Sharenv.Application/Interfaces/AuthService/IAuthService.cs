using Sharenv.Application.Models;
using Sharenv.Application.Models.Token;

namespace Sharenv.Application.Interfaces.AuthService
{
    public interface IAuthService
    {
        /// <summary>
        /// Authanticate user with cookie auth
        /// </summary>
        /// <returns></returns>
        public Result<LoginResult> LoginWithAuthCookie(string username, string password);

        /// <summary>
        /// Login user via jwt
        /// </summary>
        /// <returns></returns>
        public Result<LoginResult<GeneratedTokenResponse>> LoginWithJwt(string username, string password);

        /// <summary>
        /// Logout authanticated user
        /// </summary>
        /// <returns></returns>
        public void Logout();
    }
}
