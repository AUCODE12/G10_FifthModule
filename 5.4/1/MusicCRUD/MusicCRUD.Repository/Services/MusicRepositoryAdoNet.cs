using Microsoft.Data.SqlClient;
using MusicCRUD.DataAccess.Entity;
using MusicCRUD.Repository.Settings;
using System.Data;

namespace MusicCRUD.Repository.Services;

public class MusicRepositoryAdoNet : IMusicRepository
{
    private readonly string _connectionString;

    public MusicRepositoryAdoNet(SqlDBConeectionString sqlDBConeectionString)
    {
        _connectionString = sqlDBConeectionString.ConnectionString; 
    }

    public async Task<long> AddMusicAsync(Music music)
    {
        //With Query
        var sql = @"INSERT INTO Music (Name, MB, AuthorName, Description, QuentityLikes)
                OUTPUT Inserted.MusicId
                VALUES (@Name, @MB, @AuthorName, @Description, @QuentityLikes)";

        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@Name", music.Name);
                cmd.Parameters.AddWithValue("@MB", music.MB);
                cmd.Parameters.AddWithValue("@AuthorName", music.AuthorName);
                cmd.Parameters.AddWithValue("@Description", music.Description);
                cmd.Parameters.AddWithValue("@QuentityLikes", music.QuentityLikes);

                var res = (long)await cmd.ExecuteScalarAsync();
                //await con.CloseAsync();
                return res;
            }
        }
    }

    public async Task DeleteMusicAsync(long id)
    {
        var sql = @"DELETE FROM Music WHERE MusicId = @Id;";
        
        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<List<Music>> GetAllMusicAsync()
    {
        //With Query
        var sql = @"SELECT MusicId, Name, MB, AuthorName, Description, QuentityLikes FROM Music;";
        var music = new List<Music>();

        using (var com = new SqlConnection(_connectionString))
        {
            await com.OpenAsync();
            using (var cmd = new SqlCommand(sql, com))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    music.Add(new Music
                    {
                        MusicId = reader.GetInt64(0),
                        Name = reader.GetString(1),
                        MB = reader.GetDouble(2),
                        AuthorName = reader.GetString(3),
                        Description = reader.GetString(4),
                        QuentityLikes = reader.GetInt32(5)
                    });
                }
            }
        }
        return music;
    }

    public async Task<Music> GetMusicByIdAsync(long id)
    {
        var sql = @"SELECT MusicId, Name, MB, AuthorName, Description, QuentityLikes FROM Music WHERE MusicId = @Id";
        using (var com = new SqlConnection(_connectionString))
        {
            await com.OpenAsync();
            using (var cmd = new SqlCommand(sql, com))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Music
                        {
                            MusicId = reader.GetInt64(0),
                            Name = reader.GetString(1),
                            MB = reader.GetDouble(2),
                            AuthorName = reader.GetString(3),
                            Description = reader.GetString(4),
                            QuentityLikes = reader.GetInt32(5)
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }

    public async Task UpdateMusicAsync(Music music)
    {
        //With Query
        var sql = @"UPDATE Music
                SET Name = @Name, Mb = @MB, AuthorName = @AuthorName, Description = @Description, QuentityLikes = @QuentityLikes
                WHERE MusicId = @MusicId";

        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@MusicId", music.MusicId);
                cmd.Parameters.AddWithValue("@Name", music.Name);
                cmd.Parameters.AddWithValue("@MB", music.MB);
                cmd.Parameters.AddWithValue("@AuthorName", music.AuthorName);
                cmd.Parameters.AddWithValue("@Description", music.Description);
                cmd.Parameters.AddWithValue("@QuentityLikes", music.QuentityLikes);
                
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
