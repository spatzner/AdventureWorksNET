using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Domain.Person.DTOs
{
    public class ValidationError
    {
        public ValidationType Type { get; set; }
        public required string Field { get; set; }
        public object? Value { get; set; }
        public required string Requirements { get; set; }
        internal Stack<string> PropertyStack { get; set; } = new();
        public string PropertyHierarchy => string.Join(".", PropertyStack);
    }
}
