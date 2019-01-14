using AddressManagement.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject.Models.AccountViewModels
{
    public class RegisterViewModelTests
    {
        [Fact]
        public void Create()
        {
            var address = new RegisterViewModel();
        }
    }
}
