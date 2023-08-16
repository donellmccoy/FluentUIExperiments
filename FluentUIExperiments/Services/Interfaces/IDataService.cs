using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using FluentUIExperiments.Models;

namespace FluentUIExperiments.Services.Interfaces;

public interface IDataService
{
    Task<IReadOnlyList<County>> GetCountiesAsync(CancellationToken token = default);
    Task<IReadOnlyList<TypeOfInstrument>> GetTypesOfInstrumentsAsync(CancellationToken token = default);
    Task<IReadOnlyList<TypeOfWork>> GetTypesOfWorkAsync(CancellationToken token = default);
    Task<IReadOnlyList<TypeOfCountBy>> GetTypesOfCountBysAsync(CancellationToken token = default);
}