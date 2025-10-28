
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Sharenv.Domain.Entities
{
    public class AuditableEntity : BaseEntity
    {
        /// <summary>
        /// Gets or sets createdBy 
        /// </summary>
        [IgnoreDataMember]
        [XmlIgnore]
        [JsonIgnore]
        public virtual int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets createdAt
        /// </summary>
        [IgnoreDataMember]
        [XmlIgnore]
        [JsonIgnore]
        public virtual DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets updatedBy
        /// </summary>
        [IgnoreDataMember]
        [XmlIgnore]
        [JsonIgnore]
        public virtual int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets updatedAt
        /// </summary>
        [IgnoreDataMember]
        [XmlIgnore]
        [JsonIgnore]
        public virtual DateTime? UpdatedAt { get; set; }
    }
}
