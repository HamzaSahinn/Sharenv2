
namespace Sharenv.Application.Configurations
{
    public class DbConfiguration
    {
        /// <summary>
        /// Section name of the configuration
        /// </summary>
        public const string SECTION_NAME = "DatabseConfiguration";

        /// <summary>
        /// Gets or sets connection string
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
