using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.Entities;

namespace AdventureWorks.Domain.Person.Repositories
{
    public interface IPhoneRepository
    {
        Task Add(int personId, PhoneNumber phoneNumber);
    }
}
