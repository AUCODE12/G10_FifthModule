using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicCRUD.DataAccess;
using MusicCRUD.DataAccess.Entity;

namespace MusicCRUD.Repository.Services;

public class MusicRepository : IMusicRepository
{
    private readonly MainContext _mainContext;

    public MusicRepository(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<long> AddMusicAsync(Music music)
    {
        await _mainContext.Music.AddAsync(music);
        await _mainContext.SaveChangesAsync();

        return music.MusicId;
    }

    public async Task DeleteMusicAsync(long id)
    {
        var music = await GetMusicByIdAsync(id);
        _mainContext.Music.Remove(music);
        await _mainContext.SaveChangesAsync();
    }

    public async Task<List<Music>> GetAllMusicAsync()
    {
        var allMusic = await _mainContext.Music.ToListAsync();
        return allMusic;
    }

    public async Task<Music> GetMusicByIdAsync(long id)
    {
        var music = await _mainContext.Music.FirstOrDefaultAsync(m => m.MusicId == id);
        if(music == null)
        {
            throw new Exception($"Music not found : {id}");
        }

        return music;
    }

    public async Task UpdateMusicAsync(Music music)
    {
        throw new NotImplementedException();
    }
}
