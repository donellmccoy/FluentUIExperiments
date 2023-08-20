using System;
using System.Collections.Generic;
using System.Linq;
using FluentUIExperiments.Models;

namespace FluentUIExperiments.Extensions;

public static class FilterInformationExtensions
{
    public static List<County> GetCounties(this IReadOnlyList<FilterData> filterInformation)
    {
        return filterInformation.Where(p => Predicate(p, "Counties")).Select(p => new County
        {
            CountyId = p.Id,
            Name = p.Name

        }).ToList();
    }

    public static List<TypeOfInstrument> GetTypesOfInstruments(this IReadOnlyList<FilterData> filterInformation)
    {
        return filterInformation.Where(p => Predicate(p, "TypeOfInstruments")).Select(p => new TypeOfInstrument
        {
            TypeOfInstrumentId = p.Id,
            Name = p.Name

        }).ToList();
    }

    public static List<TypeOfWork> GetTypesOfWork(this IReadOnlyList<FilterData> filterInformation)
    {
        return filterInformation.Where(p => Predicate(p, "TypeOfWorks")).Select(p => new TypeOfWork
        {
            TypeOfWorkId = p.Id,
            Name = p.Name

        }).ToList();
    }

    public static List<TypeOfCountBy> GetTypesOfCountBy(this IReadOnlyList<FilterData> filterInformation)
    {
        return filterInformation.Where(p => Predicate(p, "TypeOfCountBys")).Select(p => new TypeOfCountBy
        {
            TypeOfCountById = p.Id,
            Name = p.Name

        }).ToList();
    }

    private static bool Predicate(FilterData data, string discriminator)
    {
        return string.Equals(data.Discriminator, discriminator, StringComparison.OrdinalIgnoreCase);
    }
}