using App.Domain;
using App.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository
{
    public class ArtistRepository : EntityRepository<Client>
    {
        public ArtistRepository(string connection) : base(connection)
        {
        }

        public override List<Client> getAll()
        {
            var query = "SELECT * FROM Artists";
            return ExecuteQuery(query, ClientMapper);
        }

        public static Client ClientMapper(IDataReader reader)
        {

            var id = reader.GetInt32(reader.GetOrdinal("Id"));

            int usernameOrdinal = reader.GetOrdinal("username");
            var username = reader.IsDBNull(usernameOrdinal) ? null : reader.GetString(usernameOrdinal);

            int passwordOrdinal = reader.GetOrdinal("password");
            var password = reader.IsDBNull(passwordOrdinal) ? null : reader.GetString(passwordOrdinal);

            int emailOrdinal = reader.GetOrdinal("email");
            var email = reader.IsDBNull(emailOrdinal) ? null : reader.GetString(emailOrdinal);

            int saltOrdinal = reader.GetOrdinal("salt");
            var salt = reader.IsDBNull(saltOrdinal) ? null : reader.GetString(saltOrdinal);

            int companyNameOrdinal = reader.GetOrdinal("companyName");
            var companyName = reader.IsDBNull(companyNameOrdinal) ? null : reader.GetString(companyNameOrdinal);

            int contactEmailOrdinal = reader.GetOrdinal("contactEmail");
            var contactEmail = reader.IsDBNull(contactEmailOrdinal) ? null : reader.GetString(contactEmailOrdinal);

            int businessEmailOrdinal = reader.GetOrdinal("businessEmail");
            var businessEmail = reader.IsDBNull(businessEmailOrdinal) ? null : reader.GetString(businessEmailOrdinal);

            int artistNameOrdinal = reader.GetOrdinal("artistName");
            var artistName = reader.IsDBNull(artistNameOrdinal) ? null : reader.GetString(artistNameOrdinal);

            Client client = new Client(id, username, password, email, salt, artistName);

            return client;
        }

        public override bool Add(Client entity)
        {
            return true;
        }

        public override bool Update(Client entity)
        {
            return true;
        }

        public override bool Delete(Client entity)
        {
            return true;
        }


        public override Client getById(int id)
        {
            var query = "SELECT * FROM Artists WHERE Id = @Id";
            var parameters = new SqlParameter[] { new SqlParameter("@Id", id) };
            return ExecuteQuery(query, ClientMapper, parameters).FirstOrDefault();
        }


    }
}
