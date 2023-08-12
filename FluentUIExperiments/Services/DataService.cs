using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentUIExperiments.Models;
using FluentUIExperiments.Options;
using FluentUIExperiments.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;

namespace FluentUIExperiments.Services;

public class DataService : IDataService
{
    #region Fields

    private readonly IOptions<AppSettings> _options;
    private readonly ILogger<DataService> _logger;

    #endregion

    #region Constructors

    public DataService(IOptions<AppSettings> options, ILogger<DataService> logger)
    {
        _options = options;
        _logger = logger;
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<County>> GetCountiesAsync(CancellationToken token = default)
    {
        return await ExecuteTaskAsync(() =>
        {
            return Task.Run(() => new List<County>
            {
                new() { CountyId = 1, Name = "Miami/Dade" },
                new() { CountyId = 2, Name = "Orange" },
                new() { CountyId = 3, Name = "Brevard" },
            }, token);
        });
    }

    public async Task<IEnumerable<TypeOfInstrument>> GetTypesOfInstrumentsAsync(CancellationToken token = default)
    {
        return await ExecuteTaskAsync(() =>
        {
            return Task.Run(() => new List<TypeOfInstrument>
            {
                new() { TypeOfInstrumentId = 1, Name = "TypeOfInstrument 1" },
                new() { TypeOfInstrumentId = 2, Name = "TypeOfInstrument 2" },
                new() { TypeOfInstrumentId = 3, Name = "TypeOfInstrument 3" },
            }, token);
        });
    }

    public async Task<IEnumerable<TypeOfWork>> GetTypesOfWorkAsync(CancellationToken token = default)
    {
        return await ExecuteTaskAsync(() =>
        {
            return Task.Run(() => new List<TypeOfWork>
            {
                new() { TypeOfWorkId = 1, Name = "TypeOfWork 1" },
                new() { TypeOfWorkId = 2, Name = "TypeOfWork 2" },
                new() { TypeOfWorkId = 3, Name = "TypeOfWork 3" },
            }, token);
        });
    }

    public async Task<IEnumerable<TypeOfCountBy>> GetTypesOfCountByAsync(CancellationToken token = default)
    {
        return await ExecuteTaskAsync(() =>
        {
            return Task.Run(() => new List<TypeOfCountBy>
            {
                new() { TypeOfCountById = 1, Name = "TypeOfCountBy 1" },
                new() { TypeOfCountById = 2, Name = "TypeOfCountBy 2" },
                new() { TypeOfCountById = 3, Name = "TypeOfCountBy 3" },
            }, token);
        });
    }

    private async Task<TEntity> ExecuteTaskAsync<TEntity>(Func<Task<TEntity>> action)
    {
        return await Policy.Handle<AggregateException>()
                           .RetryAsync(_options.Value.DatabaseOptions.AllowedRetries, onRetry: (exception, retryCount, context) =>
                           {
                                _logger.LogError("{retryCount} retry attempt to retrieve types of count by. exception: {exception}", retryCount, exception);
                           })
                           .ExecuteAsync(action);
    }

    #endregion
}