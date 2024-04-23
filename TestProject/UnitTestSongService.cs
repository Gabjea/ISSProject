using App.Service;
using ISSTests;
using System.Collections.ObjectModel;
using TestProject.MockedClasses;

namespace TestProject
{
    public class UnitTestSongService : IDisposable
    {
        SongRepositoryMock songRepositoryMock;
        SongService SongServiceWithMockedRepository;
        public UnitTestSongService()
        {
            this.songRepositoryMock = new SongRepositoryMock();
            this.SongServiceWithMockedRepository = new SongService(songRepositoryMock);
        }

        public void Dispose() { }

        [Fact]
        public void TestSongServiceReturnsCorrectNumberOfSongs()
        {
            ObservableCollection<string> returnedSongsList = this.SongServiceWithMockedRepository.getSongs();
            Assert.Equal(songRepositoryMock.getItemCount(), returnedSongsList.Count());
        }
    }
}