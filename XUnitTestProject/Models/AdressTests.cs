using AddressManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject.Models
{
    public class AddressTests
    {
        [Fact]
        public void Create()
        {
            var address = new Address();
        }

    }
}
