
namespace Sharenv.Application.Configurations
{
    public class JwtConfiguration
    {
        /// <summary>
        /// Section name of the configuration
        /// </summary>
        public const string SECTION_NAME = "JwtConfiguration";

        /// <summary>
        /// Gets or sets secretKey
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Gets or sets auhtority
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// Gets or sets audience
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets validAudiences
        /// </summary>
        public string[] ValidAudiences { get; set; }

        /// <summary>
        /// Gets or sets validIssures
        /// </summary>
        public string[] ValidIssuers { get; set; }

        /// <summary>
        /// Gets default issuer
        /// </summary>
        public string DefaultIssuer { get { return ValidIssuers[0]; } }

        /// <summary>
        /// Default audience
        /// </summary>
        public string DefaultAudience { get { return ValidAudiences[0]; } }

        /// <summary>
        /// Gets or sets MetadataAddress
        /// </summary>
        public string MetadataAddress { get; set; }

        /// <summary>
        /// Get or sets expiretime in secs
        /// </summary>
        public int ExpireTimeInSec { get; set; }
    }
}
