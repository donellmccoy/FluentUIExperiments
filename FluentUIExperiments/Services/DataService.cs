using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentUIExperiments.Models;
using FluentUIExperiments.Options;
using FluentUIExperiments.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;

namespace FluentUIExperiments.Services;

public class DataService : IDataService
{
    #region Fields

    private readonly IDbContextFactory<DataCenterWorkflowContext> _factory;
    private readonly IOptions<AppSettings> _options;
    private readonly ILogger<DataService> _logger;

    #endregion

    #region Constructors

    public DataService(IDbContextFactory<DataCenterWorkflowContext> factory, IOptions<AppSettings> options, ILogger<DataService> logger)
    {
        _factory = factory;
        _options = options;
        _logger = logger;
    }

    #endregion

    #region Methods

    public async Task<IEnumerable<County>> GetCountiesAsync(CancellationToken token = default)
    {
        return await ExecuteWithRetryAsync(async () =>
        {
            await using var context = await _factory.CreateDbContextAsync(token);
            var entities = await context.Counties.ToListAsync(token);

            return entities;
        });
    }

    public async Task<IEnumerable<TypeOfInstrument>> GetTypesOfInstrumentsAsync(CancellationToken token = default)
    {
        return await ExecuteWithRetryAsync(async () =>
        {
            await using var context = await _factory.CreateDbContextAsync(token);
            var entities = await context.TypeOfInstruments.ToListAsync(token);

            return entities;
        });
    }

    public async Task<IEnumerable<TypeOfWork>> GetTypesOfWorkAsync(CancellationToken token = default)
    {
        return await ExecuteWithRetryAsync(async () =>
        {
            await using var context = await _factory.CreateDbContextAsync(token);
            var entities = await context.TypeOfWorks.ToListAsync(token);

            return entities;
        });
    }

    public async Task<IEnumerable<TypeOfCountBy>> GetTypesOfCountBysAsync(CancellationToken token = default)
    {
        return await ExecuteWithRetryAsync(async () =>
        {
            await using var context = await _factory.CreateDbContextAsync(token);
            var entities = await context.TypeOfCountBys.ToListAsync(token);

            return entities;
        });
    }

    private async Task<TEntity> ExecuteWithRetryAsync<TEntity>(Func<Task<TEntity>> action)
    {
        return await Policy.Handle<AggregateException>()
                           .RetryAsync(_options.Value.DatabaseOptions.AllowedRetries, onRetry: OnRetry)
                           .ExecuteAsync(action);

        void OnRetry(Exception exception, int retryCount, Context context)
        {
            _logger.LogError("{retryCount} retry attempt to retrieve types of count by. exception: {exception}", retryCount, exception);
        }
    }

    #endregion
}