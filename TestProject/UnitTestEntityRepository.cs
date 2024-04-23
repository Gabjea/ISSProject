using App.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.MockedClasses;

namespace TestProject
{
    public class UnitTestEntityRepository:IDisposable
    {
        EntityRepositoryMock entityRepo;

        public UnitTestEntityRepository()
        {
            entityRepo=new EntityRepositoryMock("");
        }

        public void Dispose(){}

        [Fact]
        public void TestEntityRepositoryExecuteQueryMethod()
        {
            try
            {
                entityRepo.ExecuteQueryMock("SELECT * FROM TestEntities", reader => new TestEntity(), new SqlParameter("@param1", "value1"));
            }
            catch (Exception ex)
            {
                return;
            }

            Assert.True(false);
        }

        public void TestEntityRepositoryExecuteNonQueryMethod()
        {
            try
            {
                entityRepo.ExecuteNonQueryMock("SELECT * FROM TestEntities", new SqlParameter("@param1", "value1"));
            }
            catch (Exception ex)
            {
                return;
            }

            Assert.True(false);
        }

    }
}
