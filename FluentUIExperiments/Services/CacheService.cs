using FluentUIExperiments.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using FluentUIExperiments.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Threading;
using FluentUIExperiments.Options;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using Meziantou.Framework;

namespace FluentUIExperiments.Services;

public class CacheService : ICacheService
{
    #region Fields

    private readonly IMemoryCache _cache;
    private readonly IDataService _dataService;
    private readonly ILogger<CacheService> _logger;
    private readonly CacheOptions _cacheOptions;

    #endregion

    #region Constructors

    public CacheService(IMemoryCache cache, IDataService dataService, IOptions<AppSettings> options, ILogger<CacheService> logger)
    {
        _cache = cache;
        _dataService = dataService;
        _logger = logger;
        _cacheOptions = options.Value.CacheOptions;
    }

    #endregion

    #region Methods

    public async Task LoadCacheAsync()
    {
        await TaskEx.WhenAll
        (
            GetCountiesAsync(),
            GetTypesOfInstrumentsAsync(),
            GetTypesOfWorkAsync(),
            GetTypesOfCountByAsync()
        );
    }

    public async Task<IEnumerable<County>> GetCountiesAsync(CancellationToken token = default)
    {
        return await _cache.GetOrCreateAsync("counties", entry =>
        {
            _logger.LogInformation("caching counties");

            entry.AbsoluteExpirationRelativeToNow = _cacheOptions.AbsoluteExpirationRelativeToNow;

            return _dataService.GetCountiesAsync(token);
        });
    }

    public async Task<IEnumerable<TypeOfInstrument>> GetTypesOfInstrumentsAsync(CancellationToken token = default)
    {
        return await _cache.GetOrCreateAsync("types_of_instruments", entry =>
        {
            _logger.LogInformation("caching types of instruments");

            entry.AbsoluteExpirationRelativeToNow = _cacheOptions.AbsoluteExpirationRelativeToNow;

            return _dataService.GetTypesOfInstrumentsAsync(token);
        });
    }

    public async Task<IEnumerable<TypeOfWork>> GetTypesOfWorkAsync(CancellationToken token = default)
    {
        return await _cache.GetOrCreateAsync("types_of_work", entry =>
        {
            _logger.LogInformation("caching types of work");

            entry.AbsoluteExpirationRelativeToNow = _cacheOptions.AbsoluteExpirationRelativeToNow;

            return _dataService.GetTypesOfWorkAsync(token);
        });
    }

    public async Task<IEnumerable<TypeOfCountBy>> GetTypesOfCountByAsync(CancellationToken token = default)
    {
        return await _cache.GetOrCreateAsync("types_of_count_by", entry =>
        {
            _logger.LogInformation("caching types of count by");

            entry.AbsoluteExpirationRelativeToNow = _cacheOptions.AbsoluteExpirationRelativeToNow;

            return _dataService.GetTypesOfCountByAsync(token);
        });
    }

    #endregion
}
