using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Domain.Person
{
    public enum ValidationType
    {
        MinLength,
        MaxLength,
        UniqueList,
        IsNotEmpty,
        Min,
        Required,
        DiscreetValue
    }
}
