using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Domain.Person.DTOs
{
    public class ValidationResult
    {
        public List<ValidationError> Errors { get;} = new();
        public bool IsValid => !Errors.Any();
    }
}
