using App.Domain;
using App.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UnitTestUserRepository : IDisposable
    {
        UserRepository userRepository;
        string connectionString;
        public UnitTestUserRepository() 
        {
            connectionString = "data source=DESKTOP-LM13HS3\\SQLEXPRESS;initial catalog=ISS;trusted_connection=true;Integrated Security=true;TrustServerCertificate=True;";
            userRepository = new UserRepository(connectionString);
        }

        public void Dispose()
        {
            ClearTestData();
        }

        private void ClearTestData()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM Users;";
                cmd.ExecuteNonQuery();
            }
        }

        public User createMockUser()
        {
            Random random = new Random();

            int randomId = random.Next(50, 100001);
            return new User(randomId, "da", "admin", "admin@ubbcluj.ro", "da", "location", 10, "bronze", false);
        }

        [Fact]
        public void Add_ShouldAddUserSuccessfuly_GetById_ShouldReturnUser_GetByUsername_ShouldReturnUser()
        {
            var expectedUser = createMockUser();
            userRepository.Add(expectedUser);

            var newUserId = expectedUser.id;
            var newUserUsername = expectedUser.username;
            Assert.True(newUserId > 0);

            User actualUser = userRepository.getById(newUserId);

            Assert.Equal(expectedUser.id, actualUser.id);



           Dispose();
        }

        [Fact]
        public void GetAll_ShouldReturnTwoUsers()
        {
            var user1 = createMockUser();
            userRepository.Add(user1);

            var user2 = createMockUser();
            user2.age = 15;
            userRepository.Add(user2);

            var users = userRepository.getAll();

            Assert.True(users.Count == 2);

            Dispose();
        }

        [Fact]
        public void Update_ShouldUpdateUserSong()
        {
            var user = createMockUser();
            userRepository.Add(user);

            user.age = 15;
            userRepository.Update(user);

            var actualUser = userRepository.getById(user.id);

            Assert.Equal(15, actualUser.age);

            Dispose();
        }

        [Fact]
        public void Delete_ShouldRemoveUser()
        {
            var user = createMockUser();
            userRepository.Add(user);

            var deleteResult = userRepository.Delete(user);

            Assert.True(deleteResult);
            Assert.Null(userRepository.getById(user.id));
        }
    }
}
