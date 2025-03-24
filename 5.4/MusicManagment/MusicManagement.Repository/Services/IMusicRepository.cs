using MusicManagement.DataAccess.Entities;

namespace MusicManagement.Repository.Services;

public interface IMusicRepository
{
    Task<long> AddMusicAsync(Music music);

    Task<List<Music>> GetAllMusicAsync();

    Task<Music> GetMusicByIdAsync(Guid id); 

    Task UpdateMusicAsync(Music updatedMusic);

    Task DeleteMusicAsync(Guid id);
}