using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentUIExperiments.Models;

namespace FluentUIExperiments.Services.Interfaces;

public interface IDataService
{
    Task<IEnumerable<County>> GetCounties(CancellationToken token = default);
    Task<IEnumerable<TypeOfInstrument>> GetTypesOfInstruments(CancellationToken token = default);
    Task<IEnumerable<TypeOfWork>> GetTypesOfWork(CancellationToken token = default);
    Task<IEnumerable<TypeOfCountBy>> GetTypesOfCountBy(CancellationToken token = default);
}