using Sharenv.Application.Models;
using Sharenv.Domain.Entities;

namespace Sharenv.Application.Interfaces
{
    public interface IUserEntityService : IEntityService<User>
    {
        /// <summary>
        /// Register a new user
        /// </summary>
        /// <returns></returns>
        public Result<User> Register(User user);

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Result<User> FindByUsername(string username);

        /// <summary>
        /// Is login valid
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Result<User> IsValidLogin(string username, string password);


    }
}
