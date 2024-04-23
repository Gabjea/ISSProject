using App.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.MockedClasses
{

    public class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EntityRepositoryMock : EntityRepository<TestEntity>
    {

        protected readonly string _connectionString;

        public List<TestEntity> ExecuteQueryMock(string query, Func<IDataReader, TestEntity> mapper, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        var entities = new List<TestEntity>();

                        while (reader.Read())
                        {
                            entities.Add(mapper(reader));
                        }

                        return entities;
                    }
                }
            }
        }

        public bool ExecuteNonQueryMock(string query, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    command.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public EntityRepositoryMock(string connectionString) : base("")
        {
        }

        public override bool Add(TestEntity entity)
        {
            return true;
        }

        public override bool Delete(TestEntity entity)
        {
            return true;
        }

        public override List<TestEntity> getAll()
        {
            return [];
        }

        public override TestEntity getById(int id)
        {
            return new TestEntity();
        }

        public override bool Update(TestEntity entity)
        {
            return true;
        }
    }
}
