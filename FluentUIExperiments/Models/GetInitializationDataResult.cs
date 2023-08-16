using System.Collections.Generic;

namespace FluentUIExperiments.Models;

public class GetInitializationDataResult
{
    public List<TypeOfWork> TypeOfWorks
    {
        get; set;
    }

    public List<TypeOfInstrument> TypeOfInstruments
    {
        get; set;
    }

    public List<County> Counties
    {
        get; set;
    }

    public List<TypeOfCountBy> TypeOfCountBys
    {
        get; set;
    }
}