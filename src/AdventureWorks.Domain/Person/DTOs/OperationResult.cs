using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Domain.Person.DTOs
{
    public class OperationResult : ValidationResult
    {
        public bool Success { get; set; }
        
    }
}
