using Sharenv.Application.Interfaces;
using Sharenv.Application.Validation;
using Sharenv.Domain.Entities;
using Sharenv.Domain.Models;
using Sharenv.Infra.Data;

namespace Sharenv.Infra.Service
{
    public class UserService : EntityService<User>, IUserEntityService
    {
        public UserService(SharenvDbContext repository) : base(repository)
        {

        }

        /// <summary>
        /// Find user by Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Result<User> FindByUsername(string username)
        {
            return Execute<User>(res =>
            {
                res.Value = _repositroy.User.FirstOrDefault(x => x.Username == username);
            });
        }

        /// <summary>
        /// Is  login request valid
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Result IsValidLogin(string username, string password)
        {
            return Execute(res =>
            {
                var user = _repositroy.User.FirstOrDefault(x => x.Username == username && x.Password == password);
                if(user == null)
                {
                    res.AddError($"User {username} not found");
                    return;
                }

                if (!user.IsEmailVerified)
                {
                    res.AddError($"User must validate email address");
                    return;
                }
            });
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Result<User> Register(User user)
        {
            return Execute<User>(res =>
            {
                ArgumentValidation.ThrowIfPositive(user.Id);
                ArgumentValidation.ThrowIfNullOrWhiteSpace(user.Username);
                ArgumentValidation.ThrowIfNullOrWhiteSpace(user.Password);
                ArgumentValidation.ThrowIfNullOrWhiteSpace(user.Name);
                ArgumentValidation.ThrowIfNullOrWhiteSpace(user.Surname);

                if(FindByUsername(user.Username) != null)
                {
                    res.AddError($"Username {user.Username} already taken");
                    return;
                }

                // TODO: Apply password rules

                user.IsEmailVerified = true; // Development purposes

                res.Value = _repositroy.User.Add(user).Entity;

                _repositroy.SaveChanges();
            });
        }
    }
}
