using System.ComponentModel.DataAnnotations;

namespace Sharenv.Api.Models.Auth
{
    public class LoginRequestModel
    {
        /// <summary>
        /// Gets or sets username
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "Username cannot be more than 50 characther")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets password
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "assword cannot be more then 50 characther")]
        public string Password { get; set; }
    }
}
