﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
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
        return await ExecuteTaskWithPolicyAsync(async () =>
        {
            _logger.LogInformation("getting counties");

            await using var context = new DataCenterWorkflowContext();
            var counties = await context.Counties.ToListAsync(token);

            return counties;
        });
    }

    public async Task<IEnumerable<TypeOfInstrument>> GetTypesOfInstrumentsAsync(CancellationToken token = default)
    {
        return await ExecuteTaskWithPolicyAsync(async () =>
        {
            await using var context = new DataCenterWorkflowContext();
            var typeOfInstruments = await context.TypeOfInstruments.ToListAsync(token);

            return typeOfInstruments;
        });
    }

    public async Task<IEnumerable<TypeOfWork>> GetTypesOfWorkAsync(CancellationToken token = default)
    {
        return await ExecuteTaskWithPolicyAsync(async () =>
        {
            await using var context = new DataCenterWorkflowContext();
            var typeOfWorks = await context.TypeOfWorks.ToListAsync(token);

            return typeOfWorks;
        });
    }

    public async Task<IEnumerable<TypeOfCountBy>> GetTypesOfCountByAsync(CancellationToken token = default)
    {
        return await ExecuteTaskWithPolicyAsync(async () =>
        {
            await using var context = new DataCenterWorkflowContext();
            var typeOfCountBys = await context.TypeOfCountBys.ToListAsync(token);

            return typeOfCountBys;
        });
    }

    private async Task<TEntity> ExecuteTaskWithPolicyAsync<TEntity>(Func<Task<TEntity>> action)
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