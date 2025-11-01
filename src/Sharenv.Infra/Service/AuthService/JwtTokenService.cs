using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sharenv.Application.Configurations;
using Sharenv.Application.Interfaces.AuthService;
using Sharenv.Application.Models.Token;
using Sharenv.Application.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sharenv.Infra.Service.AuthService
{
    public class JwtTokenService : SharenvBaseService, IJwtTokenService
    {
        private JwtConfiguration _config;

        public JwtTokenService(IOptions<AuthConfiguration> options)
        {
            _config = options.Value.JwtConfiguration;
        }

        /// <summary>
        /// Geenrate token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public GeneratedTokenResponse GenerateToken(List<Claim> claims)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.SecretKey));

            var dateTimeNow = DateTime.UtcNow;
            var expireTime = dateTimeNow.Add(TimeSpan.FromSeconds(_config.ExpireTimeInSec));

            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: _config.DefaultIssuer,
                    audience: _config.DefaultAudience,
                    claims: claims,
                    notBefore: dateTimeNow,
                    expires: expireTime,
                    signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
                );

            return new GeneratedTokenResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                ExpireTime = expireTime
            };
        }
    }
}
