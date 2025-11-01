namespace Sharenv.Application.Configurations
{
    public class CookieConfiguration
    {
        public const string SECTION_NAME = "CookieConfiguration";

        /// <summary>
        /// Gets or sets expire time of auth cookie
        /// </summary>
        public int ExpireTimeInSec {  get; set; }
    }
}
