using Sharenv.Application.Models;

namespace Sharenv.Application.Interfaces.AuthService
{
    public interface ICookieAuthService
    {
        /// <summary>
        /// Authanticate user
        /// </summary>
        /// <returns></returns>
        public Result<LoginResult> Login(string username, string password);

        /// <summary>
        /// Logout authanticated user
        /// </summary>
        /// <returns></returns>
        public void Logout();
    }
}
