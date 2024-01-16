using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Common.Validation;

public interface IValidator<in T> where T : IValidatable
{
    public ValidationResult Validate(T entity);
}