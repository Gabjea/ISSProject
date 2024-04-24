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
            mockedConnectionString = "";
            songRepository = new SongRepository(mockedConnectionString);
        }
        public void Dispose() { }

        public Song createMockSong()
        {
            return new Song(1, "mockTitle", "mockArtist", "mockAlbum", new List<String>(), 0, 0, 0, 0, 0);
        }

        [Fact]
        public void TestSongRepositoryGetByIdMethod()
        {
            try
            {
                songRepository.getById(1);
            }
            catch (Exception ex) {
                return;
            }

            Assert.True(false);
        }

        [Fact]
        public void TestSongRepositoryGetAllMethod()
        {
            try
            {
               List<Song> returnedListOfSongs =  songRepository.getAll();
            }
            catch(Exception ex) { return; }

            Assert.True(false);
        }

        [Fact]
        public void TestSongRepositoryAddMethod()
        {
            try
            {
                songRepository.Add(createMockSong());
            }
            catch(Exception) { return; }

            Assert.True(false);
        }

        [Fact]
        public void TestSongRepositoryUpdateMethod()
        {
            try
            {
                songRepository.Update(createMockSong());
            }
            catch (Exception) { return; }

            Assert.True(false);
        }

        [Fact]
        public void TestSongRepositoryDeleteMethod()
        {
            try
            {
                songRepository.Delete(createMockSong());
            }
            catch (Exception) { return; }

            Assert.True(false);
        }


    }
}
