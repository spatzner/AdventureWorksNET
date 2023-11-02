namespace AdventureWorks.SqlRepository.Entities
{
    public class PersonPhone
    {
        public int BusinessEntityId { get; set; }
        public string? PhoneNumber { get; set; }
        public int PhoneNumberTypeId { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

