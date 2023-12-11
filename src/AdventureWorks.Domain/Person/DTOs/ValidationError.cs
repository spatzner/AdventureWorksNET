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
        public ValidationType ValidationType { get; set; }
        public required string Field { get; set; }
        public object? Value { get; set; }
        public required string Requirements { get; set; }
        private Stack<string> PropertyStack { get; } = new();
        public string PropertyHierarchy => string.Join(".", PropertyStack);

        public void AddToPropertyHierarchy(string propertyName)
        {
            PropertyStack.Push(propertyName);
        }
    }
}
