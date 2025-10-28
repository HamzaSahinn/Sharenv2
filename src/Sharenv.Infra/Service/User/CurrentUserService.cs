using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Sharenv.Application.Interfaces;
using Sharenv.Domain.Entities;
using System.Security.Claims;

namespace Sharenv.Infra.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private User _currentuser;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Get current user id or zero
        /// </summary>
        public int CurrentUserId { get { return CurrentUser != null ? CurrentUser.Id : 0; } }

        /// <summary>
        /// Get current user or null
        /// </summary>
        public User CurrentUser
        {
            get
            {
                if(_currentuser == null)
                {
                    var idClaim = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);

                    if (idClaim != null && int.TryParse(idClaim.Value, out int userId))
                    {
                        var svc = _httpContextAccessor?.HttpContext?.RequestServices.GetRequiredService<IUserEntityService>();
                        if (svc != null)
                        {
                            _currentuser = svc.GetById(userId).Value;
                        }
                    }
                }

                return _currentuser;
            }
        }
    }
}
