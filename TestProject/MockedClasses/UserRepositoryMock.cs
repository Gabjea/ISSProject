using App.Domain;
using App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.MockedClasses
{
    internal class UserRepositoryMock : UserRepository
    {
        private List<User> users;

        public UserRepositoryMock() : base("")
        {
            users = new List<User>();
        }

        public override bool Add(User user)
        {
            this.users.Add(user);
            return true;
        }

        public override User getByUsername(string username)
        {
            return this.users.Find(user => user.username == username);
        }

        public List<User> getUsers()
        {
            return this.users;
        }
    }
}
