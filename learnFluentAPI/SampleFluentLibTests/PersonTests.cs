using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleFluentLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleFluentLib.Tests
{
    [TestClass()]
    public class PersonTests
    {
        [TestMethod()]
        public void SingleProperty_AssertFail()
        {
            var person = new Person();

            var ex = Assert.ThrowsException<ArgumentException>(() => {
                person.ShouldNotifyFor(x => x.FirstName)
                    .When(() => person.FirstName = "John");
            });
        }
    }
}