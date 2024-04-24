using App.Repository;
using App.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;


namespace App.Repository
{
    public class ContractRepository : EntityRepository<Contract>
    {


        public ContractRepository(string connectionString) : base(connectionString)
        {
        }

        public override Contract getById(int id)
        {
            var query = "SELECT * FROM Contract WHERE id = @Id";
            var parameters = new SqlParameter[] { new SqlParameter("@Id", id) };
            return ExecuteQuery(query, ContractMapper, parameters).FirstOrDefault();
        }

        public override List<Contract> getAll()
        {
            var query = "SELECT * FROM Contract";
            return ExecuteQuery(query, ContractMapper, null);
        }

        public override bool Add(Contract contract)
        {
            var query = "INSERT INTO Contract (id,clientId1,clientId2,musicId) VALUES (@id,@clientId1,@clientId2,@musicId)";
            
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@id", contract.id),
                new SqlParameter("@clientId1", contract.client1),
                new SqlParameter("@clientId2", contract.client2),
                new SqlParameter("@musicId", contract.song)
            };

            return ExecuteNonQuery(query, parameters);
        }

        public override bool Update(Contract contract)
        {
            var querry = "UPDATE Contract SET musicId = @musicId WHERE id = @Id";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@musicId", contract.song),
                new SqlParameter("@Id", contract.id)
            };
            return ExecuteNonQuery(querry, parameters);
        }

        public override bool Delete(Contract contract)
        {
            var query = "DELETE FROM Contract WHERE Id = @Id";
            var parameters = new SqlParameter[] { new SqlParameter("@Id", contract.id) };
            return ExecuteNonQuery(query, parameters);
        }

        public static Contract ContractMapper(IDataReader reader)
        {
            var id = (int)reader["id"];
            var client1 = (int)reader["clientId1"];
            var client2 = (int)reader["clientId2"];
            var songId = (int)reader["musicId"];
            var contract = new Contract(id,client1,client2, songId);
            return contract;
        }


    }
}