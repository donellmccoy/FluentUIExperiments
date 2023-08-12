using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentUIExperiments.Models;

namespace FluentUIExperiments.Services.Interfaces;

public interface ICacheService
{
    Task<IEnumerable<County>> GetCountiesAsync(CancellationToken token = default);
    Task<IEnumerable<TypeOfInstrument>> GetTypesOfInstrumentsAsync(CancellationToken token = default);
    Task<IEnumerable<TypeOfWork>> GetTypesOfWorkAsync(CancellationToken token = default);
    Task<IEnumerable<TypeOfCountBy>> GetTypesOfCountByAsync(CancellationToken token = default);
    Task LoadCacheAsync();
}