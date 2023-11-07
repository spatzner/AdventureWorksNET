using System.Xml.Linq;
using AdventureWorks.Domain.Person;

namespace AdventureWorks.SqlRepository.DTO
{
    public interface IMappable<Dto, Entity>
    {
        static abstract Dto FromEntity(Entity entity);
        Entity ToEntity();

    }


    public class Person : IMappable<Person, Domain.Person.Person>
    {

        public int? BusinessEntityId { get; set; }

        public string? PersonType { get; set; }

        public string? Title { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? Suffix { get; set; }

        public int EmailPromotion { get; set; }

        public DateTime ModifiedDate { get; set; }


        public static Person FromEntity(Domain.Person.Person entity)
        {
            return new Person
            {
                BusinessEntityId = entity.Id,
                
            };

        }

        public Domain.Person.Person ToEntity()
        {
            return new Domain.Person.Person
            {
                Id = BusinessEntityId,
                Name = new PersonName
                {
                    Title = Title,
                    FirstName = FirstName ?? string.Empty,
                    MiddleName = MiddleName,
                    LastName = LastName ?? string.Empty,
                    Suffix = Suffix
                },
                PersonType = PersonType ?? string.Empty,
                LastModified = ModifiedDate
            };
        }
    }
}