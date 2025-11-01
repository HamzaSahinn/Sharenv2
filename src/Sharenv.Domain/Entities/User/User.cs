using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Sharenv.Domain.Entities
{
    public class User : AuditableEntity
    {
        /// <summary>
        /// Gets or sets username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or set password
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        [IgnoreDataMember]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets emailVerified
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        [IgnoreDataMember]
        public bool IsEmailVerified { get; set; }

        /// <summary>
        /// Gets or sets name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets full name name + surname
        /// </summary>
        [NotMapped]
        public string FullName { get { return Name + Surname; } }
    }
}
