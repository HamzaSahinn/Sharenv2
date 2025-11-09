

using System.ComponentModel.DataAnnotations.Schema;

namespace Sharenv.Domain.Entities.DocumentConfig
{
    public class DocumentConfiguration
    {
        /// <summary>
        /// Gets or sets storage type
        /// </summary>
        [NotMapped]
        public DocumentStorageType StorageTypeEnum { get { return (DocumentStorageType)StorageType; } set { StorageType = (int)value; } }

        /// <summary>
        /// Gets or set stroage type
        /// </summary>
        public int StorageType {  get; set; }

        /// <summary>
        /// Gets or sets document type
        /// </summary>
        [NotMapped]
        public DocumentType TypeEnum { get { return (DocumentType)Type; } set { Type = (int)value; } }

        /// <summary>
        /// Gets or sets document type
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets path
        /// </summary>
        public string Path { get; set; }

        //TODO: Add needed fields for cloud storage options
    }
}
