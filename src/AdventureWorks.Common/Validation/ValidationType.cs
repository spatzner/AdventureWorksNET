using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Common.Validation;

public enum ValidationType
{
    MinLength,
    MaxLength,
    UniqueList,
    IsNotEmpty,
    MinValue,
    Required,
    DiscreetValue,
    Range
}