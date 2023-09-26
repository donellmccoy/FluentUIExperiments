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
            .Where(data => string.Equals(data.Discriminator, DiscriminatorNames.County, StringComparison.OrdinalIgnoreCase))
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
            .Where(data => string.Equals(data.Discriminator, DiscriminatorNames.TypeOfInstrument, StringComparison.OrdinalIgnoreCase))
            .Select(data => new TypeOfInstrument
            {
                TypeOfInstrumentId = data.Id,
                Name = data.Name,
                Description = data.Description,
                IsEnabled = data.IsEnabled,
                IsVisible = data.IsVisible
            });
    }

    public static IEnumerable<TypeOfWork> GetTypesOfWork(this IReadOnlyList<FilterData> filterData)
    {
        return filterData
            .Where(data => string.Equals(data.Discriminator, DiscriminatorNames.TypeOfWork, StringComparison.OrdinalIgnoreCase))
            .Select(data => new TypeOfWork
            {
                TypeOfWorkId = data.Id,
                Name = data.Name,
                Description = data.Description,
                IsEnabled = data.IsEnabled,
                IsVisible = data.IsVisible
            });
    }

    public static IEnumerable<TypeOfCountBy> GetTypesOfCountBy(this IReadOnlyList<FilterData> filterData)
    {
        return filterData
            .Where(data => string.Equals(data.Discriminator, DiscriminatorNames.TypeOfCountBy, StringComparison.OrdinalIgnoreCase))
            .Select(data => new TypeOfCountBy
            {
                TypeOfCountById = data.Id,
                Name = data.Name,
                Description = data.Description,
                IsEnabled = data.IsEnabled,
                IsVisible = data.IsVisible
            });
    }

    private static class DiscriminatorNames
    {
        public const string County = nameof(County);

        public const string TypeOfInstrument = nameof(TypeOfInstrument);

        public const string TypeOfWork = nameof(TypeOfWork);

        public const string TypeOfCountBy = nameof(TypeOfCountBy);
    }
}