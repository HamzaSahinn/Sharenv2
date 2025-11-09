using Sharenv.Application.Interfaces;
using Sharenv.Application.Interfaces.Document;
using Sharenv.Application.Models;
using Sharenv.Application.Service;

namespace Sharenv.Infra.Service
{
    public class MomentDocumentService : SharenvBaseService, IMomentDocumentService
    {
        protected IDocumentStorageService _storageService;
        public MomentDocumentService(IEnumerable<IDocumentStorageService> services)
        {
            //TODO: get document strategy for moment documents and get suitable storage service via type parameter
        }

        public Result Delete(int momentId, int userId)
        {
            throw new NotImplementedException();
        }

        public Result<byte[]> Get(int momentId, int userId)
        {
            throw new NotImplementedException();
        }

        public Result Upload(int momentId, byte[] data, int userId)
        {
            throw new NotImplementedException();
        }

        public Result Upload(int momentId, Stream data, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
