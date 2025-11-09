using Sharenv.Application.Models;

namespace Sharenv.Application.Interfaces
{
    public interface IMomentDocumentService
    {
        public Result Upload(int momentId, byte[] data, int userId);

        public Result Upload(int momentId, Stream data, int userId);

        public Result Delete(int momentId, int userId);

        public Result<byte[]> Get(int momentId, int userId);
    }
}
