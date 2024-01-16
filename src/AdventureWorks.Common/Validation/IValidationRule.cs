using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Common.Validation;

public interface IValidationRule
{
    bool IsValid(string propertyName, object? value, [NotNullWhen(false)] out ValidationError? result);

    bool IsInvalid(string propertyName, object? value, [NotNullWhen(true)] out ValidationError? result);
}