using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.Domain.Person.Entities
{
    public abstract class Entity
    {
        public ValidationResult Validate()
        {
            ValidationResult result = new();

            Type type = GetType();

            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var propName = propertyInfo.Name;

                foreach (Attribute customAttribute in propertyInfo.GetCustomAttributes(typeof(PropertyValidationAttribute)))
                {
                    var valAttr = (PropertyValidationAttribute)customAttribute;
                    var value = propertyInfo.GetValue(this);

                    if (valAttr.IsValid(propName, value))
                        continue;

                    result.Errors.Add(valAttr.GetErrorMessage(propertyInfo.Name, value));

                }

                if (propertyInfo.PropertyType != typeof(Entity))
                    continue;
                    
                object? val = propertyInfo.GetValue(this);

                if (val == null)
                    continue;

                result.Errors.AddRange(((Entity)val).Validate().Errors.Select(err =>
                {
                    err.PropertyStack.Push(propName);
                    return err;
                }));
            }

            foreach (Attribute customAttribute in type.GetCustomAttributes(typeof(EntityValidationAttribute)))
            {
                var valAttr = (EntityValidationAttribute)customAttribute;

                if (valAttr.IsValid(this))
                    continue;

                result.Errors.Add(valAttr.GetErrorMessage());
            }

            return result;
        }
    }

   
}





