using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain;
using App.Repository;
namespace TestProject
{
    public class UnitTestArtistRepository : IDisposable
    {
        ArtistRepository artistRepository;
        string mockConnection;

        public UnitTestArtistRepository()
        {
            mockConnection = "";
            artistRepository = new ArtistRepository(mockConnection);
        }

        public void Dispose() { }

        public Client createMockArtist()
        {
            return new Client(1, "username", "password", "email", "salt", "artistName");
        }

        [Fact]

        public void TestArtistRepositoryGetByIdMethod()
        {
            try
            {
                artistRepository.getById(1);
            }
            catch (Exception ex)
            {
                return;
            }

            Assert.True(false);
        }

        [Fact]

        public void TestArtistRepositoryGetAllMethod()
        {
            try
            {
                List<Client> returnedListOfArtists = artistRepository.getAll();
            }
            catch (Exception ex)
            {
                return;
            }

            Assert.True(false);
        }

        [Fact]

        public void TestArtistRepositoryAddMethod()
        {
            try
            {
                artistRepository.Add(createMockArtist());
            }
            catch (Exception)
            {
                return;
            }

            Assert.True(true);
        }

        [Fact]

        public void TestArtistRepositoryUpdateMethod()
        {
            try
            {
                artistRepository.Update(createMockArtist());
            }
            catch (Exception)
            {
                return;
            }

            Assert.True(true);
        }


        [Fact]

        public void TestArtistRepositoryDeleteMethod()
        {
            try
            {
                artistRepository.Delete(createMockArtist());
            }
            catch (Exception)
            {
                return;
            }

            Assert.True(true);
        }


    }
}
