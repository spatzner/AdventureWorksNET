using Microsoft.SqlServer.Types;

namespace AdventureWorks.SqlRepository.Entities;

public class Address
{
    public int BusinessEntityId { get; set;}
    public string AddressType { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Couunty { get; set; }
    public string PostalCode { get; set; }
    public  SqlGeography SpatialLocation { get; set; }
    public DateTime ModifiedDate { get; set; }

}