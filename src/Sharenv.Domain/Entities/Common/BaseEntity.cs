
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Sharenv.Domain.Entities
{
    public class BaseEntity : IEntity
    {
        /// <summary>
        /// Gets Id and protected sets Id
        /// </summary>
        [IgnoreDataMember]
        [XmlIgnore]
        [JsonIgnore]
        public virtual int Id { get; protected set; }
    }
}
