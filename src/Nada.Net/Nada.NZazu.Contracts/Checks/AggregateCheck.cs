using System;
using System.Collections.Generic;
using System.Linq;

namespace Nada.NZazu.Contracts.Checks;

/// <inheritdoc cref="IValueCheck"/>
public class AggregateCheck : IValueCheck
{
    private readonly List<IValueCheck> _checks;


    /// <inheritdoc cref="IValueCheck"/>
    public AggregateCheck(IEnumerable<IValueCheck> checks)
    {
        _checks = (checks ?? Enumerable.Empty<IValueCheck>()).ToList();
    }

    /// <inheritdoc cref="IValueCheck"/>
    public IEnumerable<IValueCheck> Checks => _checks;

    /// <inheritdoc cref="IValueCheck"/>
    public ValueCheckResult Validate(string value, object parsedValue, IFormatProvider formatProvider)
    {
        var invalidChecks = _checks
            .Select(c => c.Validate(value, parsedValue, formatProvider) ?? ValueCheckResult.Success)
            .Where(x => !x.IsValid)
            .ToArray();

        return invalidChecks.Length switch
        {
            0 => ValueCheckResult.Success,
            1 => invalidChecks.First(),
            _ => new AggregateValueCheckResult(invalidChecks)
        };
    }
}