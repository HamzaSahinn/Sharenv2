
namespace Sharenv.Application.Models
{
    public enum LoginFailedReason
    {
        /// <summary>
        /// User does not exists password or usernmae not valid
        /// </summary>
        NotFound,

        /// <summary>
        /// Email verification failed
        /// </summary>
        MailNotValidated,

        /// <summary>
        /// password expired user must change password
        /// </summary>
        PasswordExpired
    }
}
