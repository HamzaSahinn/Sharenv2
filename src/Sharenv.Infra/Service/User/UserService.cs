using Microsoft.EntityFrameworkCore;
using Sharenv.Application.Interfaces;
using Sharenv.Domain.Entities;
using Sharenv.Domain.Models;

namespace Sharenv.Infra.Service
{
    public class UserService : EntityService<User>, IUserEntityService
    {
        public UserService(DbContext repository) : base(repository)
        {

        }

        public Result<User> FindByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Result IsValidLogin(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Result<User> Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}
