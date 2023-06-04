using System;
using Nada.NZazu.Contracts.Checks;

namespace Nada.NZazu.Contracts.FormChecks;

public interface IFormCheck
{
    ValueCheckResult Validate(FormData formData, IFormatProvider formatProvider = null);
}