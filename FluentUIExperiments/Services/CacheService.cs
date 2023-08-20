using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentUIExperiments.Models;
using FluentUIExperiments.Options;
using FluentUIExperiments.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
        await GetFilterDataAsync();
    }

    public async Task<IReadOnlyList<FilterData>> GetFilterDataAsync(CancellationToken token = default)
    {
        return await _cache.GetOrCreateAsync(CacheKeys.FilterData, entry =>
        {
            entry.SetAbsoluteExpiration(_cacheOptions.AbsoluteExpiration);
            return _dataService.GetFilterInformation(token);
        });
    }

    public async Task<IReadOnlyList<County>> GetCountiesAsync(CancellationToken token = default)
    {
        return await _cache.GetOrCreateAsync(CacheKeys.Counties, entry =>
        {
            entry.SetAbsoluteExpiration(_cacheOptions.AbsoluteExpiration);
            return _dataService.GetCountiesAsync(token);
        });
    }

    public async Task<IReadOnlyList<TypeOfInstrument>> GetTypesOfInstrumentsAsync(CancellationToken token = default)
    {
        return await _cache.GetOrCreateAsync(CacheKeys.TypesOfInstruments, entry =>
        {
            entry.SetAbsoluteExpiration(_cacheOptions.AbsoluteExpiration);
            return _dataService.GetTypesOfInstrumentsAsync(token);
        });
    }

    public async Task<IReadOnlyList<TypeOfWork>> GetTypesOfWorkAsync(CancellationToken token = default)
    {
        return await _cache.GetOrCreateAsync(CacheKeys.TypesOfWork, entry =>
        {
            entry.SetAbsoluteExpiration(_cacheOptions.AbsoluteExpiration);
            return _dataService.GetTypesOfWorkAsync(token);
        });
    }

    public async Task<IReadOnlyList<TypeOfCountBy>> GetTypesOfCountBysAsync(CancellationToken token = default)
    {
        return await _cache.GetOrCreateAsync(CacheKeys.TypesOfCountBys, entry =>
        {
            entry.SetAbsoluteExpiration(_cacheOptions.AbsoluteExpiration);
            return _dataService.GetTypesOfCountBysAsync(token);
        });
    }

    #endregion

    #region Classes

    private static class CacheKeys
    {
        public const string Counties = $"_{nameof(Counties)}";

        public const string TypesOfInstruments = $"_{nameof(TypesOfInstruments)}";

        public const string TypesOfWork = $"_{nameof(TypesOfWork)}";

        public const string TypesOfCountBys = $"_{nameof(TypesOfCountBys)}";

        public const string FilterData = $"_{nameof(FilterData)}";
    }

    #endregion
}
