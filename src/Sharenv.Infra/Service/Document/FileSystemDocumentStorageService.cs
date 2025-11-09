using Sharenv.Application.Interfaces.Document;
using Sharenv.Application.Models;
using Sharenv.Application.Service;
using Sharenv.Application.Validation;
using Sharenv.Domain.Entities.DocumentConfig;

namespace Sharenv.Infra.Service.Document
{
    public class FileSystemDocumentStorageService : SharenvBaseService, IDocumentStorageService
    {
        public DocumentStorageType Type { get; private set; } = DocumentStorageType.FileSystem;

        public FileSystemDocumentStorageService() { }

        /// <summary>
        /// Delete file
        /// </summary>
        /// <param name="pathIdentifier"></param>
        /// <returns></returns>
        public Result Delete(string pathIdentifier)
        {
            return Execute(res =>
            {
                ArgumentValidation.ThrowIfNullOrEmpty(pathIdentifier);

                if (File.Exists(pathIdentifier))
                {
                    File.Delete(pathIdentifier);
                }
                else
                {
                    res.AddError($"Document not found at {pathIdentifier}");
                }
            });
        }

        /// <summary>
        /// Get file data
        /// </summary>
        /// <param name="pathIdentifier"></param>
        /// <returns></returns>
        public Result<byte[]> Get(string pathIdentifier)
        {
            return Execute<byte[]>(res =>
            {
                ArgumentValidation.ThrowIfNullOrEmpty(pathIdentifier);

                if (File.Exists(pathIdentifier))
                {
                    byte[] data = File.ReadAllBytes(pathIdentifier);
                }
                else
                {
                    res.AddError($"Document not found at {pathIdentifier}");
                }
            });
        }

        /// <summary>
        /// Gets file as stream
        /// </summary>
        /// <param name="pathIdentifier"></param>
        /// <returns></returns>
        public Result<Stream> GetStream(string pathIdentifier)
        {
            return Execute<Stream>(res =>
            {
                if (File.Exists(pathIdentifier))
                {
                    res.Value = new FileStream(pathIdentifier, FileMode.Open, FileAccess.Read, FileShare.Read);
                }
                else
                {
                    res.AddError($"Document not found with path {pathIdentifier}");
                }
            });
        }

        /// <summary>
        /// Upload file data to path
        /// </summary>
        /// <param name="pathIdentifier"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Result Upload(string pathIdentifier, byte[] data, string? mimeType)
        {
            return Execute(res =>
            {
                ArgumentValidation.ThrowIfNullOrEmpty(data);

                string directory = Path.GetDirectoryName(pathIdentifier);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllBytes(pathIdentifier, data);
            });
        }

        /// <summary>
        /// Upload file stream to file system
        /// </summary>
        /// <param name="pathIdentifier"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Result Upload(string pathIdentifier, Stream data, string? mimeType)
        {
            return Execute(res =>
            {
                ArgumentValidation.ThrowIfNull(data);
                ArgumentValidation.ValidateStreamReadable(data);

                string directory = Path.GetDirectoryName(pathIdentifier);

                // Create necessary directories
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var fileStream = new FileStream(pathIdentifier, FileMode.Create, FileAccess.Write))
                {
                    data.CopyTo(fileStream);
                }
            });
        }

        /// <summary>
        /// Check file exists
        /// </summary>
        /// <param name="pathIdentifier"></param>
        /// <returns></returns>
        public Result<bool> Exists(string pathIdentifier)
        {
            return Execute<bool>(res =>
            {
                res.Value = File.Exists(pathIdentifier);
            });
        }
    }
}
