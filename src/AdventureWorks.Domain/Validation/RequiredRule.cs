using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.DTOs;

namespace AdventureWorks.Domain.Validation
{
    public class RequiredRule :  IValidationRule
    {
        public bool Validate(string propertyName, object? obj, out ValidationError? result)
        {
            var valid = obj != null;

            result = valid ? default : GetErrorMessage(propertyName, obj);
            
            return obj != null;
        }


        public ValidationError GetErrorMessage(string propertyName, object? value)
        {
            throw new NotImplementedException();
        }
    }
}
