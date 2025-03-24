using MusicCRUD.DataAccess.Entity;
using System.Text.Json;

namespace MusicCRUD.Repository.Services;

public class MusicRepositoryFile : IMusicRepository
{
    private readonly string _filePath;
    private readonly string _directoryPath;
    private List<Music> _music;
    public MusicRepositoryFile()
    {
        _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Music.json");
        _directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");

        if(!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }

        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }

        _music = GetAllMusicAsync().Result;
    }

    public async Task<long> AddMusicAsync(Music music)
    {
        _music.Add(music);
        await SaveDataAsync();
        return music.MusicId;
    }

    public async Task DeleteMusicAsync(long id)
    {
        var music = await GetMusicByIdAsync(id);
        _music.Remove(music);
        await SaveDataAsync();
    }

    public async Task<List<Music>> GetAllMusicAsync()
    {
        var musicJson = await File.ReadAllTextAsync(_filePath);
        var musicList = JsonSerializer.Deserialize<List<Music>>(musicJson);
        return musicList ?? new List<Music>();
    }

    public async Task<Music> GetMusicByIdAsync(long id)
    {
        var music = _music.FirstOrDefault(x => x.MusicId == id);
        if (music == null)
        {
            throw new Exception($"Music with id {id} not found");
        }

        return music;
    }

    public async Task UpdateMusicAsync(Music music)
    {
        var musicFromDb = await GetMusicByIdAsync(music.MusicId);
        var index = _music.IndexOf(musicFromDb);
        _music[index] = music;
        await SaveDataAsync();
    }

    private async Task SaveDataAsync()
    {
        var musicJson = JsonSerializer.Serialize(_music);
        await File.WriteAllTextAsync(_filePath, musicJson);
    }
}

