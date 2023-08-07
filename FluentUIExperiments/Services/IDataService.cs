using System.Collections.Generic;
using System.Threading.Tasks;
using FluentUIExperiments.Models;

namespace FluentUIExperiments.Services;

public interface IDataService
{
    Task<IEnumerable<County>> GetCounties();
    Task<IEnumerable<TypeOfInstrument>> GetTypesOfInstruments();
    Task<IEnumerable<TypeOfWork>> GetTypesOfWork();
    Task<IEnumerable<TypeOfCountBy>> GetTypesOfCountBy();
}