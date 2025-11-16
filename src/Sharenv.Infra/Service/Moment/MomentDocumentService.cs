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

        /// <summary>
        /// Delete moment data
        /// </summary>
        /// <param name="momentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Result Delete(int momentId, int userId)
        {
            return Execute(res =>
            {
                string rootPath = ResolvePath(momentId, _documentConfig.Path);

                _documentStorageService.Delete(rootPath).CopyTo(res);
            });
        }

        /// <summary>
        /// Get moment data
        /// </summary>
        /// <param name="momentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result<byte[]> Get(int momentId, int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Upload moment data
        /// </summary>
        /// <param name="momentId"></param>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result Upload(int momentId, byte[] data, int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Upload moment data
        /// </summary>
        /// <param name="momentId"></param>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result Upload(int momentId, Stream data, int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resolve path from id and base path
        /// </summary>
        /// <param name="momentId"></param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        private string ResolvePath(int momentId, string basePath)
        {
            string paddedId = momentId.ToString($"D{12}");

            var pathChunks = new List<string>();
            for (int i = 0; i < paddedId.Length; i += 3)
            {
                pathChunks.Add(paddedId.Substring(i, 3));
            }

            string[] pathSegments = new string[pathChunks.Count + 1];
            pathSegments[0] = basePath;
            pathChunks.CopyTo(pathSegments, 1);

            return Path.Combine(pathSegments);
        }
    }
}
