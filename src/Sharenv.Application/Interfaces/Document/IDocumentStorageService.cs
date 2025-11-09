using Sharenv.Application.Models;
using Sharenv.Domain.Entities.DocumentConfig;

namespace Sharenv.Application.Interfaces.Document
{
    public interface IDocumentStorageService
    {
        public DocumentStorageType Type { get; }

        public Result Upload(string pathIdentifier, byte[] data, string? mimeType);

        public Result Upload(string pathIdentifier, Stream data, string? mimeType);

        public Result Delete(string pathIdentifier);

        public Result<byte[]> Get(string pathIdentifier);

        public Result<Stream> GetStream(string pathIdentifier);

        public Result<bool> Exists(string pathIdentifier);
    }
}
