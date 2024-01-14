using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Validation;

public abstract class Validator<T>(IValidationBuilder validationBuilder) : IValidator<T> where T : IValidatable
{
    protected IValidationBuilder ValidationBuilder { get; } = validationBuilder;

    public abstract ValidationResult Validate(T entity);
}