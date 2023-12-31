﻿using System;
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
using Polly.Retry;

namespace FluentUIExperiments.Services;

public class DataService : IDataService
{
    #region Fields

    private readonly IDbContextFactory<ApplicationDbContext> _factory;
    private readonly IOptions<AppSettings> _options;
    private readonly ILogger<DataService> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;

    #endregion

    #region Constructors

    public DataService(IDbContextFactory<ApplicationDbContext> factory, 
        IOptions<AppSettings> options, 
        ILogger<DataService> logger)
    {
        _factory = factory;
        _options = options;
        _logger = logger;

        _retryPolicy = Policy.Handle<AggregateException>()
            .Or<Exception>()
            .RetryAsync(_options.Value.DatabaseOptions.AllowedRetries, (exception, retryCount, _) =>
            {
                _logger.LogError("{retryCount} retry attempt to retrieve filter data. exception: {exception}", retryCount, exception);
            });
    }

    #endregion

    #region Methods

    public async Task<IReadOnlyList<FilterData>> GetFilterDataAsync(CancellationToken token = default)
    {
        return await ExecuteWithRetryAsync(async () =>
        {
            await using var context = await _factory.CreateDbContextAsync(token);
            return await context.GetFilterDataAsync(token);
        });
    }

    public async Task<IReadOnlyList<County>> GetCountiesAsync(CancellationToken token = default)
    {
        return await ExecuteWithRetryAsync(async () =>
        {
            await using var context = await _factory.CreateDbContextAsync(token);
            return await context.Counties.AsNoTracking().ToListAsync(token);
        });
    }

    public async Task<IReadOnlyList<TypeOfInstrument>> GetTypesOfInstrumentsAsync(CancellationToken token = default)
    {
        return await ExecuteWithRetryAsync(async () =>
        {
            await using var context = await _factory.CreateDbContextAsync(token);
            return await context.TypeOfInstruments.AsNoTracking().ToListAsync(token);
        });
    }

    public async Task<IReadOnlyList<TypeOfWork>> GetTypesOfWorkAsync(CancellationToken token = default)
    {
        return await ExecuteWithRetryAsync(async () =>
        {
            await using var context = await _factory.CreateDbContextAsync(token);
            return await context.TypesOfWork.AsNoTracking().ToListAsync(token);
        });
    }

    public async Task<IReadOnlyList<TypeOfCountBy>> GetTypesOfCountBysAsync(CancellationToken token = default)
    {
        return await ExecuteWithRetryAsync(async () =>
        {
            await using var context = await _factory.CreateDbContextAsync(token);
            return await context.TypeOfCountBys.AsNoTracking().ToListAsync(token);
        });
    }

    private async Task<TEntity> ExecuteWithRetryAsync<TEntity>(Func<Task<TEntity>> action)
    {
        return await _retryPolicy.ExecuteAsync(action);
    }

    #endregion
}