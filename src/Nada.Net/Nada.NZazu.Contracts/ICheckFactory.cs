using System;
using System.Collections.Generic;
using Nada.NZazu.Contracts.Checks;
using Nada.NZazu.Contracts.FormChecks;

namespace Nada.NZazu.Contracts;

public interface ICheckFactory
{
    IEnumerable<string> AvailableTypes { get; }

    IValueCheck CreateCheck(
        CheckDefinition checkDefinition,
        FieldDefinition fieldDefinition,
        Func<FormData> formData = null,
        INZazuTableDataSerializer tableSerializer = null,
        int rowIdx = -1);

    IFormCheck CreateFormCheck(CheckDefinition checkDefinition);
}