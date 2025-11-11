using Sharenv.Application.Interfaces;
using Sharenv.Application.Interfaces.Document;
using Sharenv.Application.Models;
using Sharenv.Application.Service;
using Sharenv.Domain.Entities.DocumentConfig;
using Sharenv.Infra.Service.Document;

namespace Sharenv.Infra.Service
{
    public class MomentDocumentService : SharenvBaseService, IMomentDocumentService
    {
        protected IDocumentStorageService _storageService;

        protected DocumentConfiguration _documentConfig;

        protected IDocumentStorageService _documentStorageService;

        public MomentDocumentService(IEnumerable<IDocumentStorageService> services)
        {
            _documentConfig = DocumentConfigurationService.CachedConfigurations.Values.FirstOrDefault(x => x.TypeEnum == DocumentType.Moment);
            _documentStorageService = services.FirstOrDefault(x => x.Type == _documentConfig.StorageTypeEnum);
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
