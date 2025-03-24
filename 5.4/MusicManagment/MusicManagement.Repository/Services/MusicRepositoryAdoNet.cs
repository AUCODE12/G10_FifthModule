using Microsoft.Data.SqlClient;
using MusicManagement.DataAccess.Entities;
using MusicManagement.Repository.Settings;

namespace MusicManagement.Repository.Services;

public class MusicRepositoryAdoNet : IMusicRepository
{
    private readonly string _connectionString;

    public MusicRepositoryAdoNet(SqlDBConnectionString sqlDBConnectionString)
    {
        _connectionString = sqlDBConnectionString.ConnectionString;
    }

    public async Task<long> AddMusicAsync(Music music)
    {
        //With Query
        var sql = @"INSERT INTO Music (Name, MB, AuthorName, Description, QuentityLikes)
                OUTPUT Inserted.Id
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

    public Task DeleteMusicAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Music>> GetAllMusicAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Music> GetMusicByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateMusicAsync(Music updatedMusic)
    {
        throw new NotImplementedException();
    }
}
