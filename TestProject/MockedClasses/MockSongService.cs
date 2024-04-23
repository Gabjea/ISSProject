using App.Repository;
using App.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.MockedClasses
{
    public class MockSongService : SongService
    {
        public MockSongService() : base(null) // Pass null or a mock of SongRepository if needed
        {
        }

        public override ObservableCollection<string> getSongs()
        {
            // Mock implementation of getSongs method
            return new ObservableCollection<string>(new List<string> { "MockedSong1", "MockedSong2" });
        }
    }
}
