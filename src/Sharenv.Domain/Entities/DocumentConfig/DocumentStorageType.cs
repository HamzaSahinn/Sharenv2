namespace Sharenv.Domain.Entities.DocumentConfig
{
    public enum DocumentStorageType
    {
        /// <summary>
        /// Files stored n file system
        /// </summary>
        FileSystem,

        /// <summary>
        /// File stored in S3 buckets
        /// </summary>
        S3,

        /// <summary>
        /// File stored in GoogleDrive
        /// </summary>
        GoogleDrive
    }
}
