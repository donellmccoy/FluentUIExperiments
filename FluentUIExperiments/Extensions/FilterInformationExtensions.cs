using System;
using System.Collections.Generic;
using System.Linq;
using FluentUIExperiments.Models;

namespace FluentUIExperiments.Extensions;

public static class FilterInformationExtensions
{
    public static IEnumerable<County> GetCounties(this IReadOnlyList<FilterData> filterData)
    {
        return filterData
            .Where(p => string.Equals(p.Discriminator, DiscriminatorNames.Counties, StringComparison.OrdinalIgnoreCase))
            .Select(data => new County
            {
                CountyId = data.Id,
                Name = data.Name,
                Description = data.Description,
                IsEnabled = data.IsEnabled,
                IsVisible = data.IsVisible
            });
    }

    public static IEnumerable<TypeOfInstrument> GetTypesOfInstruments(this IReadOnlyList<FilterData> filterData)
    {
        return filterData
            .Where(p => string.Equals(p.Discriminator, DiscriminatorNames.TypeOfInstruments, StringComparison.OrdinalIgnoreCase))
            .Select(p => new TypeOfInstrument
            {
                TypeOfInstrumentId = p.Id,
                Name = p.Name,
                Description = p.Description,
                IsEnabled = p.IsEnabled,
                IsVisible = p.IsVisible
            });
    }

    public static IEnumerable<TypeOfWork> GetTypesOfWork(this IReadOnlyList<FilterData> filterData)
    {
        return filterData
            .Where(p => string.Equals(p.Discriminator, DiscriminatorNames.TypeOfWorks, StringComparison.OrdinalIgnoreCase))
            .Select(p => new TypeOfWork
            {
                TypeOfWorkId = p.Id,
                Name = p.Name,
                Description = p.Description,
                IsEnabled = p.IsEnabled,
                IsVisible = p.IsVisible
            });
    }

    public static IEnumerable<TypeOfCountBy> GetTypesOfCountBy(this IReadOnlyList<FilterData> filterData)
    {
        return filterData
            .Where(p => string.Equals(p.Discriminator, DiscriminatorNames.TypeOfCountBys, StringComparison.OrdinalIgnoreCase))
            .Select(p => new TypeOfCountBy
            {
                TypeOfCountById = p.Id,
                Name = p.Name,
                Description = p.Description,
                IsEnabled = p.IsEnabled,
                IsVisible = p.IsVisible
            });
    }

    private static class DiscriminatorNames
    {
        public const string Counties = nameof(Counties);

        public const string TypeOfInstruments = nameof(TypeOfInstruments);

        public const string TypeOfWorks = nameof(TypeOfWorks);

        public const string TypeOfCountBys = nameof(TypeOfCountBys);
    }
}