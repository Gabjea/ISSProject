using App.Domain;
using App.Repository;
using Microsoft.Data.SqlClient;
using System;

namespace TestProject
{
    public class UnitTestPlaylistRepository : IDisposable
    {
        PlaylistRepository _playlistRepository;
        string connectionString;

        public UnitTestPlaylistRepository()
        {
            connectionString = "data source=DESKTOP-Q2V2E5Q;initial catalog=ISS;trusted_connection=true;Integrated Security=true;TrustServerCertificate=True;";
            _playlistRepository = new PlaylistRepository(connectionString);
            ClearTestData();
        }

        public void Dispose() 
        {
            ClearTestData();
        }

        private void ClearTestData()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM PlaylistMusic;" + // Assuming CASCADE DELETE is not set, you need to delete dependencies first
                                  "DELETE FROM Playlists;";
                cmd.ExecuteNonQuery();
            }
        }

        public Song createMockSong(int id)
        {
            return new Song(id, "mockTitle", "mockArtist", "mockAlbum", new List<String>(), 0, 0, 0, 0, 0);
        }

        public Playlist createMockPlaylist()
        {
            Random random = new Random();

            // Generate a random integer between 50 and 100000
            int randomId = random.Next(50, 100001);
            return new Playlist(randomId, "mockName" + randomId.ToString(), new List<Song> { createMockSong(1), createMockSong(2), createMockSong(3) });
        }

        [Fact]
        public void Add_ShouldAddPlaylistSuccessfully_GetById_ShouldReturnPlaylist()
        {
            // Arrange
            var expectedPlaylist = createMockPlaylist();
            _playlistRepository.Add(expectedPlaylist);

            var newPlaylistId = expectedPlaylist.id; // Assuming Add updates the Id property with the database generated Id
            Assert.True(newPlaylistId > 0); // Ensure ID was set indicating insertion was successful

            // Act
            Playlist actualPlaylist = _playlistRepository.getById(newPlaylistId);

            // Assert
            Assert.Equal(expectedPlaylist.name, actualPlaylist.name);

            Dispose();
        }

        [Fact]
        public void Update_ShouldUpdatePlaylistName()
        {
            // Arrange
            var playlist = createMockPlaylist();
            _playlistRepository.Add(playlist);

            playlist.name = "Updated Playlist";
            _playlistRepository.Update(playlist);

            // Act
            var actualPlaylist = _playlistRepository.getById(playlist.id);

            // Assert
            Assert.Equal("Updated Playlist", actualPlaylist.name);

            // Clean-up in Dispose
            Dispose();
        }

        [Fact]
        public void Delete_ShouldRemovePlaylist()
        {
            // Arrange
            var playlist = createMockPlaylist();
            _playlistRepository.Add(playlist);

            // Act
            var deleteResult = _playlistRepository.Delete(playlist);

            // Assert
            Assert.True(deleteResult);
            Assert.Null(_playlistRepository.getById(playlist.id)); // Assuming getById returns null if not found

            // Clean-up in Dispose is not required here as it has been deleted
        }

        [Fact]
        public void GetAll_ShouldReturnTwoPlaylists()
        {
            // Arrange
            var playlist1 = createMockPlaylist();
            _playlistRepository.Add(playlist1);

            var playlist2 = createMockPlaylist();
            playlist2.name = "Another Playlist";
            _playlistRepository.Add(playlist2);

            // Act
            var playlists = _playlistRepository.getAll();

            // Assert
            Assert.True(playlists.Count == 2); // We've added 2 playlists, so expect 2

            // Clean-up in Dispose
            Dispose();
        }

        [Fact]
        public void AddSongToPlaylist_ShouldAddSongSuccessfully()
        {
            var playlist = createMockPlaylist();
            _playlistRepository.Add(playlist);
            // Perform
            var result = _playlistRepository.AddSongToPlaylist(playlist.id, 1);
            Assert.True(result);

            // Validation
            var playlistWithSongs = _playlistRepository.GetPlaylistWithSongs(playlist.id);
            Assert.Contains(playlistWithSongs.songs, song => song.id == 1);

            // Clean-up in Dispose
            Dispose();
        }

        [Fact]
        public void GetPlaylistWithSongs_ShouldReturnSongs()
        {
            var playlist = createMockPlaylist();
            _playlistRepository.Add(playlist);
            // Pre-condition: Add a song to the playlist
            var result = _playlistRepository.AddSongToPlaylist(playlist.id, 1);
            Assert.True(result);

            // Test
            var playlistWithSongs = _playlistRepository.GetPlaylistWithSongs(playlist.id);
            Assert.NotNull(playlistWithSongs);
            Assert.True(playlistWithSongs.songs.Any(song => song.id == 1));

            // Clean-up in Dispose
            Dispose();
        }

        [Fact]
        public void RemoveSongFromPlaylist_ShouldRemoveSongSuccessfully()
        {
            // Pre-condition: Ensure the song is added first
            var playlist = createMockPlaylist();
            _playlistRepository.Add(playlist);
            // Pre-condition: Add a song to the playlist
            _playlistRepository.AddSongToPlaylist(playlist.id, 1);

            // Perform
            var result = _playlistRepository.removeSongFromPlaylist(playlist.id, 1);
            Assert.True(result);

            // Validation
            var playlistWithSongs = _playlistRepository.GetPlaylistWithSongs(playlist.id);
            Assert.False(playlistWithSongs.songs.Any(song => song.id == 1));

            // Clean-up in Dispose
            Dispose();
        }

/*        [Fact]
        public void GetPlaylistsByUser_ShouldReturnUserPlaylists()
        {
            // Pre-condition: You should ensure there's a relationship setup between User and Playlist for the testUserId
            var playlist = new Playlist(100, "mockName" + 100.ToString(), new List<Song> { createMockSong(1), createMockSong(2), createMockSong(3) });
            _playlistRepository.Add(playlist);

            // Perform
            var playlists = _playlistRepository.GetPlaylistsByUser(_testUserId);
            Assert.IsTrue(playlists.Any(playlist => playlist.Id == _testPlaylistId));

            // Cleanup: Optional cleanup if the AddPlaylistToUser modifies data that must be reverted.
        }*/
    }
}
