using MusicManagement.DataAccess.Entities;
using System.Text.Json;

namespace MusicManagement.Repository.Services;

public class MusicBrokerFile : IMusicRepository
{
    private string _baseFile;
    private string _baseDirectory;
    private List<Music> _music;
    public MusicBrokerFile()
    {
        _baseFile = Path.Combine(Directory.GetCurrentDirectory(), "data_base", "Music.json");
        _baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "data_base");

        if (!Directory.Exists(_baseDirectory)) Directory.CreateDirectory(_baseDirectory);
        if (!File.Exists(_baseFile)) File.WriteAllTextAsync(_baseFile, "[]");

        _music = GetAllMusicAsync().Result;
    }
    
    public async Task<Guid> AddMusicAsync(Music music)
    {
        _music.Add(music);
        await SaveDataAsync();
        return music.Id;
    }

    public async Task DeleteMusicAsync(Guid id)
    {
        var music = await GetMusicByIdAsync(id);
        _music.Remove(music);
        await SaveDataAsync();
    }

    public async Task<List<Music>> GetAllMusicAsync()
    {
        var musicJson = await File.ReadAllTextAsync(_baseFile);
        var music = JsonSerializer.Deserialize<List<Music>>(musicJson);
        return music ?? new List<Music>();
    }

    public async Task<Music> GetMusicByIdAsync(Guid id)
    {
        var music = _music.FirstOrDefault(m => m.Id == id);
        await Task.Delay(0); // 
        if (music is null) throw new Exception("not found");
        return music;
    }

    public async Task UpdateMusicAsync(Music updatedMusic)
    {
        var music = await GetMusicByIdAsync(updatedMusic.Id);
        var index = _music.IndexOf(music);
        _music[index] = updatedMusic;
        await SaveDataAsync();
    }

    private async Task SaveDataAsync()
    {
        var musicJson = JsonSerializer.Serialize(_music);
        await File.WriteAllTextAsync(_baseFile, musicJson);
    }
}
