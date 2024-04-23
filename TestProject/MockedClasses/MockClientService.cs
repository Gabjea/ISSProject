using App.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.MockedClasses
{
    public class MockClientService : ClientService
    {
        public MockClientService() : base(null /* You can pass null or mock dependencies here */)
        {
        }

        public override bool CreateAccountWrapper(string email, string username, string password, string confirmPassword, string artistName)
        {
            // Mock implementation of CreateAccount
            return true; // or any desired result
        }
    }

}
