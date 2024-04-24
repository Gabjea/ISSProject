using App.Domain;
using App.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.MockedClasses;
using Xunit.Sdk;

namespace TestProject
{
    public class UnitTestUserService: IDisposable
    {
        UserRepositoryMock userRepositoryMock;
        UserService UserServiceWithMockedRepository;
        public UnitTestUserService()
        {
            this.userRepositoryMock = new UserRepositoryMock();
            this.UserServiceWithMockedRepository = new UserService(userRepositoryMock);
        }

        public void Dispose() { }

        private bool isValidLogin(string _username, string _password)
        {
            List<User> users = userRepositoryMock.getUsers();
            foreach (var user in users)
            {
                if(user.username == _username)
                {
                    if (user.passwordHash == _password)
                        return true;
                    return false;
                }
            }
            return false;
        }

        private bool isAdmin(string username)
        {
            List<User> users = userRepositoryMock.getUsers();
            foreach (var user in users)
            {
                if (user.username == username)
                {
                    if (user.isAdmin)
                        return true;
                    return false;
                }
            }
            return false;
        }

        [Fact]
        public void TestUserServiceCreate()
        {
            Assert.False(this.UserServiceWithMockedRepository.CreateAccount(null, null, null, null, null, 0));
            Assert.False(this.UserServiceWithMockedRepository.CreateAccount("", "", "", "", null, 0));
            Assert.False(this.UserServiceWithMockedRepository.CreateAccount("da", "da", "nu", "da", "Acasa", 10));
            Assert.True(this.UserServiceWithMockedRepository.CreateAccount("da", "da", "admin", "admin", "Acasa", 10));
        }

        [Fact]
        public void TestUserServiceLoginValidation()
        {
            this.UserServiceWithMockedRepository.CreateAccount("da", "da", "admin", "admin", "Acasa", 10);
            Assert.False(isValidLogin("da", "admin"));
        }

        [Fact]
        public void TestUserServiceUserIsAdmin() 
        {
            Assert.False(isAdmin("da"));
        }
    }
}
