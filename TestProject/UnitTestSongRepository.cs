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

        public Song createMockSongNonExistentId()
        {
            return new Song(23242, "mockTitle", "mockArtist", "mockAlbum", new List<String>(), 0, 0, 0, 0, 0);
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
                Song returnedSong = songRepository.getById(995);
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
            catch (Exception) { Assert.True(false); }
        }


      /*  [Fact]
        public void TestSongRepositoryAddMethodFails()
        {
            try
            {
                songRepository.Add(createMockSong());
                Assert.True(false);
            }
            catch (Exception) { Assert.True(true); }
        }*/

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
        public void TestSongRepositoryUpdateMethodFails()
        {
            try
            {
                songRepository.Update(createMockSongNonExistentId());
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
            catch (Exception) { Assert.True(false);}
        }

        [Fact]
        public void TestSongRepositoryDeleteMethodFails()
        {
            try
            {
                songRepository.Delete(createMockSongNonExistentId());
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
                Assert.Equal(5, returnedListOfSongs.Count);
            }
            catch(Exception ex) { Assert.True(false); }
        }

    }
}
