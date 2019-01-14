using AddressManagement.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject.Models.AccountViewModels
{
    public class ExternalLoginViewModelTests
    {
        [Fact]
        public void Create()
        {
            var address = new ExternalLoginViewModel();
        }
    }
}
