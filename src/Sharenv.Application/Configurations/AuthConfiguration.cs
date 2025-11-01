namespace Sharenv.Application.Configurations
{
    public class AuthConfiguration
    {
        public const string SECTION_NAME = "AuthConfiguration";

        /// <summary>
        /// Gets or sets jwt configurations
        /// </summary>
        public JwtConfiguration JwtConfiguration { get; set; }

        /// <summary>
        /// Gets or sets auth cookie configurations
        /// </summary>
        public CookieConfiguration CookieConfiguration { get; set; }
    }
}
