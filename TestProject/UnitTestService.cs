using Xunit;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using App.Service;
using App.Domain;
using App.Repository;
using TestProject.MockedClasses;

namespace TestProject
{
    public class UnitTestService
    {
        [Fact]
        public void TestLogin_ValidUsernameAndPassword()
        {
            var mockUserRepo = new Mock<UserRepository>();
            mockUserRepo.Setup(repo => repo.getByUsername(It.IsAny<string>())).Returns(new User(1, "username", "password", "email", "salt", "location", 25, "Free", false)); // Mocking a valid user

            var mockClientRepo = new Mock<ClientRepository>();

            var userService = new Mock<UserService>();

            var clientService = new Mock<ClientService>();

            var service = new Service(null, null, mockUserRepo.Object, null, null, null, userService.Object);

            bool result = service.Login("username", "password");

            Assert.True(result);
        }

        [Fact]
        public void TestCreateAccount_IsClient()
        {
            var mockClientService = new Mock<MockClientService>();

            var service = new Service(null, null, null, null, mockClientService.Object, null, null);

            bool result = service.CreateAccount("email", "username", "password", "confirmPassword", true, artistName: "artistName");

            Assert.False(result);
        }


        [Fact]
        public void TestGetSongs()
        {
            var mockSongRepository = new Mock<SongRepository>();

            var mockSongService = new Mock<SongService>(mockSongRepository.Object);
            mockSongService.Setup(service => service.getSongs()).Returns(new ObservableCollection<string>(new List<string> { "Song1", "Song2" }));

            var service = new Service(null, null, null, mockSongService.Object, null, null, null);

            ObservableCollection<string> songs = service.GetSongs();

            Assert.Equal(2, songs.Count);
        }
    }
}
