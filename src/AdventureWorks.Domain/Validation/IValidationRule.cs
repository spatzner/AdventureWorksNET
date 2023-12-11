using AdventureWorks.Domain.Person.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Domain.Validation
{
    public interface IValidationRule
    {
        bool Validate(string propertyName, object? obj, out ValidationError? result);
        ValidationError GetErrorMessage(string propertyName, object? value);
    }
}
