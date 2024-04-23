using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Domain;
using System.Data;
using Microsoft.Data.SqlClient;
using App.Repository;

namespace App.Repository
{
    public class PlaylistRepository : EntityRepository<Playlist>
    {
        public PlaylistRepository(string connectionString) : base(connectionString)
        {
            
        }

        public override Playlist getById(int id)
        {
            var query = "SELECT * FROM Playlists WHERE id = @Id";
            var parameters = new SqlParameter[] { new SqlParameter("@Id", id) };
            return ExecuteQuery(query, PlaylistMapper, parameters).FirstOrDefault();
        }

        public override List<Playlist> getAll()
        {
            var query = "SELECT * FROM Playlists";
            return ExecuteQuery(query, PlaylistMapper);
        }

        public override bool Add(Playlist playlist)
        {
            var query = "INSERT INTO Playlists (id, name) VALUES (@id, @name)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@id", playlist.id),
                new SqlParameter("@name", playlist.name),
            };

            return ExecuteNonQuery(query, parameters);
        }

        public override bool Update(Playlist playlist)
        {
            var query = "UPDATE Playlists SET name = @name WHERE id = @Id";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", playlist.id),
                new SqlParameter("@name", playlist.name),
            };

            return ExecuteNonQuery(query, parameters);
        }

        public override bool Delete(Playlist playlist)
        {
            var query = "DELETE FROM Playlists WHERE id = @Id";
            var parameters = new SqlParameter[] { new SqlParameter("@id", playlist.id) };
            return ExecuteNonQuery(query, parameters);
        }

        public static Playlist PlaylistMapper(IDataReader reader)
        {
            var id = (int)reader["id"];
            var name = (string)reader["name"];
            var playlist = new Playlist(id, name);
            return playlist;
        }

        public Playlist GetPlaylistWithSongs(int playlistId)
        {
            Playlist playlist = null;

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                string query = "SELECT PlaylistMusic.musicId " +
                "FROM  Playlists " +
                "INNER JOIN PlaylistMusic on Playlists.id = PlaylistMusic.playlistId " +
                "WHERE Playlists.id = @playlistId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@playlistId", playlistId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Song> songs = new List<Song>();
                        while (reader.Read())
                        {
                            int songId = (int)reader["musicId"];
                            Song song = new SongRepository(_connectionString).getById(songId);
                            songs.Add(song);
                        }

                        playlist = getById(playlistId);
                        playlist.songs = songs;

                    }
                }

            }

            return playlist;
        }

        public bool AddSongToPlaylist(int playlistId, int songId)
        {
            var query = "INSERT INTO PlaylistMusic (playlistId, musicId) VALUES (@playlistId, @songId)";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@playlistId", playlistId),
                new SqlParameter("@songId", songId),
            };

            return ExecuteNonQuery(query, parameters);
        }   

        public bool removeSongFromPlaylist(int playlistId, int songId)
        {
            var query = "DELETE FROM PlaylistMusic WHERE playlistId = @playlistId AND musicId = @songId";
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@playlistId", playlistId),
                new SqlParameter("@songId", songId),
            };

            return ExecuteNonQuery(query, parameters);
        }

        /*public List<Playlist> GetPlaylistsByUser(int userId)
        {
            List<Playlist> playlists = new List<Playlist>();

            using (SqlConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                string query = "SELECT Playlists.id, Playlists.name" +
                "FROM  Playlists " +
                "INNER JOIN UserPlaylist on Playlist.id = UserPlaylist.PlaylistId" +
                "WHERE UserPlaylist.UserId = @userId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int playlistId = (int)reader["id"];
                            Playlist playlist = getById(playlistId);
                            playlists.Add(playlist);
                        }
                    }
                }

            }

            return playlists;
        }*/
    }
}