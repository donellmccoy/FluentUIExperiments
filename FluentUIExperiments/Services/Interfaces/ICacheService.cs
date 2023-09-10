using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentUIExperiments.Models;

namespace FluentUIExperiments.Services.Interfaces;

public interface ICacheService
{
    Task<IReadOnlyList<County>> GetCountiesAsync(CancellationToken token = default);
    Task<IReadOnlyList<TypeOfInstrument>> GetTypesOfInstrumentsAsync(CancellationToken token = default);
    Task<IReadOnlyList<TypeOfWork>> GetTypesOfWorkAsync(CancellationToken token = default);
    Task<IReadOnlyList<TypeOfCountBy>> GetTypesOfCountBysAsync(CancellationToken token = default);
    Task RefreshCacheAsync();
    Task<IReadOnlyList<FilterData>> GetFilterDataAsync(CancellationToken token = default);
    Task<FilterInformation> GetFilterInformationAsync(CancellationToken token = default);
}