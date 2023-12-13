using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorks.Domain.Person.DTOs
{
    public class SearchResult<T> : ValidationResult
    {
        public required List<T> Results { get; set; }
        public required int Total { get; set; }

        public SearchResult()
        {
        }

        public SearchResult(ValidationResult validationResult) : base(validationResult)
        {
        }
    }
}
