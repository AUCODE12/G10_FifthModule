using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Data.SqlClient;
using OnlineVotingSystem.Dal.Entities;
using OnlineVotingSystem.Repository.Settings;
using System.Data;

namespace OnlineVotingSystem.Repository.Services;

public class UserRepositoryAdoNet : IUserRepository
{
    private readonly string _connectionString;

    public UserRepositoryAdoNet(SqlDBConnectionString connectionString)
    {
        _connectionString = connectionString.ConnectionSting;
    }

    public async Task<long> AddUserAsync(User user)
    {
        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand("sp_AddUserAsync", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Role", user.Role);

                var outputId = new SqlParameter("@Id", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputId);

                await cmd.ExecuteNonQueryAsync();
                return Convert.ToInt64(outputId.Value);
            }
        }
    }

    public async Task DeleteUserAsync(long id)
    {
        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand("sp_DeleteUserAsync", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
    public async Task UpdateUserAsync(User updatedUser)
    {
        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand("sp_UpdateUserAsync", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", updatedUser.Id);
                cmd.Parameters.AddWithValue("@Name", updatedUser.Name);
                cmd.Parameters.AddWithValue("@Email", updatedUser.Email);
                cmd.Parameters.AddWithValue("@Role", updatedUser.Role);

                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<ICollection<User>> GetAllUsersAsync()
    {
        var users = new List<User>();

        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand("sp_GetAllUser", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(new User
                        {
                            Id = reader.GetInt64(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            Role = reader.GetString(3)
                        });
                    }
                }
            }
        }
        return users;
    }

    public async Task<User> GetUserByIdAsync(long id)
    {
        using (var con = new SqlConnection(_connectionString))
        {
            await con.OpenAsync();
            using (var cmd = new SqlCommand("sp_GetUserById", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return (new User
                        {
                            Id = reader.GetInt64(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            Role = reader.GetString(3)
                        });
                    }
                }
            }
        }
        return null;
    }

}
