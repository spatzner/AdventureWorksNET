using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Application
{
    public class ValidationService : IValidationService
    {
        private readonly Dictionary<Type, IValidator<IValidatable>> _entityValidators;
        public ValidationService(IEnumerable<IValidator<IValidatable>> entityValidators)
        {
            _entityValidators = entityValidators.ToDictionary(x => x.GetType().GetGenericParameterConstraints()[0], x => x);
        }


        public ValidationResult Validate<T>(T obj) where T : IValidatable
        {

            ValidationResult result = _entityValidators[typeof(T)].Validate(obj);

            var addlValidations = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.PropertyType.GetInterfaces().Contains(typeof(IValidatable)));

            foreach (PropertyInfo prop in addlValidations)
            {
                var propObj = prop.GetValue(obj);

                if (propObj == null)
                    continue;
                
                ValidationResult innerResult = _entityValidators[prop.PropertyType].Validate((IValidatable)propObj);

                result.Errors.AddRange(innerResult.Errors.Select(x =>
                {
                    x.AddToPropertyHierarchy(prop.Name);
                    return x;
                }));

            }

            return result;
        }
    }
}
