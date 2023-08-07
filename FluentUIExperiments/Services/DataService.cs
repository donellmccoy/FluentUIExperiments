using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentUIExperiments.Models;

namespace FluentUIExperiments.Services;

public class DataService : IDataService
{
    public async Task<IEnumerable<County>> GetCounties()
    {
        await Task.CompletedTask;

        return new List<County>
        {
            new() { CountyId = 1, Name = "Miami/Dade" },
            new() { CountyId = 2, Name = "Orange" },
            new() { CountyId = 3, Name = "Brevard" },
        };
    }

    public async Task<IEnumerable<TypeOfInstrument>> GetTypesOfInstruments()
    {
        await Task.CompletedTask;

        return new List<TypeOfInstrument>
        {
            new() { TypeOfInstrumentId = 1, Name = "TypeOfInstrument 1" },
            new() { TypeOfInstrumentId = 2, Name = "TypeOfInstrument 2" },
            new() { TypeOfInstrumentId = 3, Name = "TypeOfInstrument 3" },
        };
    }

    public async Task<IEnumerable<TypeOfWork>> GetTypesOfWork()
    {
        await Task.CompletedTask;

        return new List<TypeOfWork>
        {
            new() { TypeOfWorkId = 1, Name = "TypeOfWork 1" },
            new() { TypeOfWorkId = 2, Name = "TypeOfWork 2" },
            new() { TypeOfWorkId = 3, Name = "TypeOfWork 3" },
        };
    }

    public async Task<IEnumerable<TypeOfCountBy>> GetTypesOfCountBy()
    {
        await Task.Delay(5000);

        throw new Exception("Test exception");

        return new List<TypeOfCountBy>
        {
            new() { TypeOfCountById = 1, Name = "TypeOfCountBy 1" },
            new() { TypeOfCountById = 2, Name = "TypeOfCountBy 2" },
            new() { TypeOfCountById = 3, Name = "TypeOfCountBy 3" },
        };
    }
}