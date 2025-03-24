using Microsoft.EntityFrameworkCore;
using MusicManagement.DataAccess;
using MusicManagement.DataAccess.Entities;

namespace MusicManagement.Repository.Services;

public class MusicRepository : IMusicRepository
{
    private readonly MainContext _mainContext;

    public MusicRepository(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public async Task<Guid> AddMusicAsync(Music music)
    {
        await _mainContext.AddAsync(music);
        await _mainContext.SaveChangesAsync();
        return music.Id;
    }

    public async Task DeleteMusicAsync(Guid id)
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

    public async Task<Music> GetMusicByIdAsync(Guid id)
    {
        var music = await _mainContext.Music.FirstOrDefaultAsync(m => m.Id == id);
        if (music == null) throw new Exception();
        return music;
    }

    public async Task UpdateMusicAsync(Music updatedMusic)
    {
        var music = await GetMusicByIdAsync(updatedMusic.Id);
        _mainContext.Music.Update(music);
        await _mainContext.SaveChangesAsync();
    }
}
