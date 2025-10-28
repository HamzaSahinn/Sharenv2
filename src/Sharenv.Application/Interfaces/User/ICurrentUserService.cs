using Sharenv.Domain.Entities;

namespace Sharenv.Application.Interfaces
{
    public interface ICurrentUserService
    {
        /// <summary>
        /// Get logged in user id
        /// </summary>
        public int CurrentUserId { get; }

        /// <summary>
        /// Get current user or null
        /// </summary>
        public User CurrentUser { get; } 
    }
}
