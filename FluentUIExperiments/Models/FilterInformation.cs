using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentUIExperiments.Models;

public class FilterInformation
{
    private readonly IEnumerable<FilterData> _filterInformation;

    public FilterInformation(IEnumerable<FilterData> filterInformation)
    {
        _filterInformation = filterInformation;
    }

    public IReadOnlyList<County> Counties => _filterInformation
        .Where(p => string.Equals(p.Discriminator, DiscriminatorNames.Counties, StringComparison.OrdinalIgnoreCase))
        .Select(data => new County
        {
            CountyId = data.Id,
            Name = data.Name,
            Description = data.Description,
            IsEnabled = data.IsEnabled,
            IsVisible = data.IsVisible
        }).ToList();

    private static class DiscriminatorNames
    {
        public const string Counties = nameof(Counties);

        public const string TypeOfInstruments = nameof(TypeOfInstruments);

        public const string TypeOfWorks = nameof(TypeOfWorks);

        public const string TypeOfCountBys = nameof(TypeOfCountBys);
    }

}