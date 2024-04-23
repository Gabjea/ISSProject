using App.Domain;
using App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.MockedClasses
{
    internal class SongRepositoryMock : SongRepository
    {
        private int itemCount = 10;
        public SongRepositoryMock() : base("")
        {
         
        }

        public int getItemCount()
        {
            return itemCount;
        }

        public Song createMockSong()
        {
            return new Song(1, "mockTitle", "mockArtist", "mockAlbum", new List<String>(), 0, 0, 0, 0, 0);
        }

        public override List<Song> getAll()
        {
            List<Song> mockedListOfSongs = new List<Song>();
            for (int i = 0; i < itemCount; i++)
            {
                mockedListOfSongs.Add(createMockSong());
            }
            return mockedListOfSongs;
        }

    }
}
