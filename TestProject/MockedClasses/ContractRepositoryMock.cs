using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Repository;

namespace TestProject.MockedClasses
{
    internal class ContractRepositoryMock : ContractRepository
    {
        public ContractRepositoryMock(string connectionString) : base(connectionString)
        {

        }
    }
}
