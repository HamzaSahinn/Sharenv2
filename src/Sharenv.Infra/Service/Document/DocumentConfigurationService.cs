using System;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Sharenv.Application.Interfaces.Initializable;
using Sharenv.Domain.Entities.DocumentConfig;
using Sharenv.Infra.Data;

namespace Sharenv.Infra.Service.Document;

public class DocumentConfigurationService : EntityService<DocumentConfiguration>, IInitializable
{
    private static ConcurrentDictionary<int, DocumentConfiguration> _cachedConfigs;

    public static IReadOnlyDictionary<int, DocumentConfiguration> CachedConfigurations => _cachedConfigs;

    public DocumentConfigurationService(SharenvDbContext repository) : base(repository)
    {
        _cachedConfigs = new ConcurrentDictionary<int, DocumentConfiguration>();
    }

    /// <summary>
    /// Intialize DocumentConfiguration Service by data in db
    /// </summary>
    public void Initialize()
    {
        _cachedConfigs.Clear();

        var allConfigs = this._repositroy.DocumentConfiguration.AsNoTracking().ToList();

        foreach (var config in allConfigs)
        {
            if (config.Id != 0)
            {
                _cachedConfigs.TryAdd(config.Id, config);
            }
        }
    }
}
