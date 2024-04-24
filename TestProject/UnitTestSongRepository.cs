using App.Domain;
using App.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.MockedClasses;

namespace TestProject
{
    public class UnitTestSongRepository : IDisposable
    {
        SongRepository songRepository;
        string mockedConnectionString;
        public UnitTestSongRepository()
        {
            mockedConnectionString = "data source=FlorinPC\\SQLEXPRESS;initial catalog=ISS;trusted_connection=true;Integrated Security=true;TrustServerCertificate=true;";
            songRepository = new SongRepository(mockedConnectionString);
        }
        public void Dispose() { }

        public Song createMockSong()
        {
            return new Song(10, "mockTitle", "mockArtist", "mockAlbum", new List<String>(), 0, 0, 0, 0, 0);
        }

        public Song createMockSongNonExistingId()
        {
            return new Song(232452, "mockTitle", "mockArtist", "mockAlbum", new List<String>(), 0, 0, 0, 0, 0);
        }

        [Fact]
        public void TestSongRepositoryGetByIdMethod()
        {
            try
            {
                Song returnedSong = songRepository.getById(1);
                Assert.Equal(1, returnedSong.id);
            }
            catch (Exception ex) {
                Assert.True(false);
            }
        }

        [Fact]
        public void TestSongRepositoryGetByIdMethodFails()
        {
            try
            {
                Song returnedSong = songRepository.getById(992);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void TestSongRepositoryAddMethod()
        {
            try
            {
                songRepository.Add(createMockSong());
                Assert.True(true);
            }
            catch (Exception ex)
            {
                Assert.True(false);
                Console.WriteLine(ex.Message);
            }
        }

        [Fact]
        public void TestSongRepositoryFailsAddMethod()
        {
            try
            {
                songRepository.Add(createMockSong());
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void TestSongRepositoryUpdateMethod()
        {
            try
            {
                songRepository.Update(createMockSong());
                Assert.True(true);
            }
            catch (Exception) { Assert.True(false); }
        }

        [Fact]
        public void TestSongRepositoryFailsUpdateMethod()
        {
            try
            {
                songRepository.Update(createMockSongNonExistingId());
                Assert.True(false);
            }
            catch (Exception) { Assert.True(true); }
        }



        [Fact]
        public void TestSongRepositoryDeleteMethod()
        {
            try
            {
                songRepository.Delete(createMockSong());
                Assert.True(true);
            }
            catch (Exception) { Assert.True(false); }
        }

        [Fact]
        public void TestSongRepositoryFailsDeleteMethod()
        {
            try
            {
                songRepository.Delete(createMockSongNonExistingId());
                Assert.True(false);
            }
            catch (Exception) { Assert.True(true); }
        }

        [Fact]
        public void TestSongRepositoryGetAllMethod()
        {
            try
            {
               List<Song> returnedListOfSongs =  songRepository.getAll();
               Assert.Equal(5, returnedListOfSongs.Count());
            }
            catch(Exception) { Assert.True(false); }

        }


    }
}
