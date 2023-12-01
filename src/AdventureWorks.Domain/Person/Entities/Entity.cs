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
                ValidateProperty(propertyInfo, result);
            }

            foreach (Attribute customAttribute in type.GetCustomAttributes(typeof(EntityValidationAttribute)))
            {
                ValidateEntity(customAttribute, result);
            }

            return result;
        }

        private void ValidateProperty(PropertyInfo propertyInfo, ValidationResult result)
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
                return;

            object? subEntity = propertyInfo.GetValue(this);

            if (subEntity == null)
                return;

            result.Errors.AddRange(((Entity)subEntity).Validate().Errors.Select(err =>
            {
                err.PropertyStack.Push(propName);
                return err;
            }));
        }

        private void ValidateEntity(Attribute customAttribute, ValidationResult result)
        {
            var valAttr = (EntityValidationAttribute)customAttribute;

            if (valAttr.IsValid(this))
                return;

            result.Errors.Add(valAttr.GetErrorMessage());
        }
    }

   
}





