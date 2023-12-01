using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Domain.Person.DTOs
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public List<ValidationError> ValidationErrors { get; set; } = new();
        public bool IsValid => !ValidationErrors.Any();
        
    }
}
