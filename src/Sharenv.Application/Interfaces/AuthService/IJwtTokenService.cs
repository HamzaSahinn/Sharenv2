using Sharenv.Application.Models;
using Sharenv.Application.Models.Token;
using System.Security.Claims;

namespace Sharenv.Application.Interfaces.AuthService
{
    public interface IJwtTokenService
    {
        /// <summary>
        /// Create jwt token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public GeneratedTokenResponse GenerateToken(List<Claim> claims);
    }
}
