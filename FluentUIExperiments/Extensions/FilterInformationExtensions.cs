using System;
using System.Collections.Generic;
using System.Linq;
using FluentUIExperiments.Models;

namespace FluentUIExperiments.Extensions;

public static class FilterInformationExtensions
{
    public static IEnumerable<County> GetCounties(this IReadOnlyList<FilterData> filterInformation)
    {
        return filterInformation.Where(p => Predicate(p, DiscriminatorNames.Counties)).Select(p => new County
        {
            CountyId = p.Id,
            Name = p.Name,
            Description = p.Description,
            IsEnabled = p.IsEnabled,
            IsVisible = p.IsVisible
        });
    }

    public static IEnumerable<TypeOfInstrument> GetTypesOfInstruments(this IReadOnlyList<FilterData> filterInformation)
    {
        return filterInformation.Where(p => Predicate(p, DiscriminatorNames.TypeOfInstruments)).Select(p => new TypeOfInstrument
        {
            TypeOfInstrumentId = p.Id,
            Name = p.Name,
            Description = p.Description,
            IsEnabled = p.IsEnabled,
            IsVisible = p.IsVisible
        });
    }

    public static IEnumerable<TypeOfWork> GetTypesOfWork(this IReadOnlyList<FilterData> filterInformation)
    {
        return filterInformation.Where(p => Predicate(p, DiscriminatorNames.TypeOfWorks)).Select(p => new TypeOfWork
        {
            TypeOfWorkId = p.Id,
            Name = p.Name,
            Description = p.Description,
            IsEnabled = p.IsEnabled,
            IsVisible = p.IsVisible
        });
    }

    public static IEnumerable<TypeOfCountBy> GetTypesOfCountBy(this IReadOnlyList<FilterData> filterInformation)
    {
        return filterInformation.Where(p => Predicate(p, DiscriminatorNames.TypeOfCountBys)).Select(p => new TypeOfCountBy
        {
            TypeOfCountById = p.Id,
            Name = p.Name,
            Description = p.Description,
            IsEnabled = p.IsEnabled,
            IsVisible = p.IsVisible
        });
    }

    private static bool Predicate(FilterData data, string discriminator)
    {
        return string.Equals(data.Discriminator, discriminator, StringComparison.OrdinalIgnoreCase);
    }

    private static class DiscriminatorNames
    {
        public const string Counties = nameof(Counties);
        public const string TypeOfInstruments = nameof(TypeOfInstruments);
        public const string TypeOfWorks = nameof(TypeOfWorks);
        public const string TypeOfCountBys = nameof(TypeOfCountBys);
    }
}