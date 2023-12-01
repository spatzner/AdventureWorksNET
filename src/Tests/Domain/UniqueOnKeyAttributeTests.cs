using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks.Domain.Person.Entities;
using AdventureWorks.Domain.Validation;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;

namespace Tests.Domain
{
    [TestClass]
    public class UniqueOnKeyAttributeTests
    {
        [TestMethod]
        public void TUniqueOnKeyAttribute_WhenNonUniqueKeys_IsNotValid()
        {
            ContainerClass container = new()
            {
                KeyClasses = new List<KeyClass>
                {
                    new()
                    {
                        Name = "test",
                        Type = 1
                    },
                    new()
                    {
                        Name = "test",
                        Type = 1
                    }
                }

            };

            var result = container.Validate();

            Assert.IsFalse(result.IsValid);
        }
    }


    public class KeyClass
    {
        [ValidationKey]
        public string Name { get; set; } = string.Empty;

        [ValidationKey]
        public int Type { get; set; }
    }

    public class ContainerClass : Entity
    {
        [UniqueOnKey] public List<KeyClass> KeyClasses { get; set; } = new();
    }
}
