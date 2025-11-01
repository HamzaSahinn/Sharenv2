
using Sharenv.Domain.Entities;

namespace Sharenv.Application.Models
{
    public class LoginResult
    {
        /// <summary>
        /// Gets or sets is login sucessfull
        /// </summary>
        public bool IsSuccess {  get; set; }

        /// <summary>
        /// Gets or sets fail reason if failed
        /// </summary>
        public LoginFailedReason? FailedReason { get; set; }

        /// <summary>
        /// Gets or sets user entitiy if login successfull
        /// </summary>
        public User? User { get; set; } 
    }

    public class LoginResult<T> : LoginResult
    {
        /// <summary>
        /// Gets or sets data
        /// </summary>
        public T Data { get; set; }
    }
}
