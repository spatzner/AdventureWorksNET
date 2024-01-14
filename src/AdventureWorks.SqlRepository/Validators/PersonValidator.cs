using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.DTOs;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;

namespace AdventureWorks.SqlRepository.Validators
{
    public class PersonValidator(IRuleProvider ruleProvider) : Domain.Person.Validation.PersonValidator(ruleProvider)
    {
        public override ValidationResult Validate(Person entity)
        {
            var result = base.Validate(entity);

            //TODO: Add person type validation (oneofrule)
            //entity.PersonType

            throw new NotImplementedException();
        }
    }
}
