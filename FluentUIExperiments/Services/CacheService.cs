using FluentUIExperiments.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using FluentUIExperiments.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace FluentUIExperiments.Services;

public class CacheService : ICacheService
{
    #region Fields

    private readonly IMemoryCache _cache;
    private readonly IDataService _dataService;
    private readonly ILogger<CacheService> _logger;
    private readonly TimeSpan _absoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10000);

    #endregion

    #region Constructors

    public CacheService(IMemoryCache cache, IDataService dataService, ILogger<CacheService> logger)
    {
        _cache = cache;
        _dataService = dataService;
        _logger = logger;
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<County>> GetCounties(CancellationToken token = default)
    {
        return await _cache.GetOrCreateAsync("counties", entry =>
        {
            _logger.LogInformation("caching counties");

            entry.AbsoluteExpirationRelativeToNow = _absoluteExpirationRelativeToNow;

            return _dataService.GetCounties(token);
        });
    }

    public async Task<IEnumerable<TypeOfInstrument>> GetTypesOfInstruments(CancellationToken token = default)
    {
        return await _cache.GetOrCreateAsync("types_of_instruments", entry =>
        {
            _logger.LogInformation("caching types of instruments");

            entry.AbsoluteExpirationRelativeToNow = _absoluteExpirationRelativeToNow;

            return _dataService.GetTypesOfInstruments(token);
        });
    }

    public async Task<IEnumerable<TypeOfWork>> GetTypesOfWork(CancellationToken token = default)
    {
        return await _cache.GetOrCreateAsync("types_of_work", entry =>
        {
            _logger.LogInformation("caching types of work");

            entry.AbsoluteExpirationRelativeToNow = _absoluteExpirationRelativeToNow;

            return _dataService.GetTypesOfWork(token);
        });
    }

    public async Task<IEnumerable<TypeOfCountBy>> GetTypesOfCountBy(CancellationToken token = default)
    {
        return await _cache.GetOrCreateAsync("types_of_count_by", entry =>
        {
            _logger.LogInformation("caching types of count by");

            entry.AbsoluteExpirationRelativeToNow = _absoluteExpirationRelativeToNow;

            return _dataService.GetTypesOfCountBy(token);
        });
    }

    #endregion
}