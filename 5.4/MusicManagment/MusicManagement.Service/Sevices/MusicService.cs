using MusicManagement.DataAccess.Entities;
using MusicManagement.Repository.Services;
using MusicManagement.Service.DTOs;

namespace MusicManagement.Service.Sevices;

public class MusicService : IMusicService
{
    private readonly IMusicRepository _musicRepository;

    public MusicService(IMusicRepository musicRepository)
    {
         _musicRepository = musicRepository;
    }

    public async Task<long> AddMusicAsync(MusicDto musicDto)
    {
        var music = await _musicRepository.AddMusicAsync(ConvertToMusicEntity(musicDto));
        return music;
    }

    public async Task DeleteMusicAsync(Guid id)
    {
        await _musicRepository.DeleteMusicAsync(id);
    }

    public async Task UpdateMusicAsync(MusicDto updateMusicDto)
    {
        await _musicRepository.UpdateMusicAsync(ConvertToMusicEntity(updateMusicDto));
    }

    public async Task<MusicDto> GetMusicByIdAsync(Guid id)
    {
        var music = await _musicRepository.GetMusicByIdAsync(id);
        return ConvertToMusicDto(music);
    }

    public async Task<List<MusicDto>> GetAllMusicAsync()
    {
        var music = await _musicRepository.GetAllMusicAsync();
        return music.Select(m => ConvertToMusicDto(m)).ToList();
    }

    public async Task<List<MusicDto>> GetAllMusicByAuthorNameAsync(string name)
    {
        var music = await GetAllMusicAsync();
        return music.Where(m => m.AuthorName == name).ToList();
    }

    public async Task<MusicDto> GetMostLikedMusicAsync()
    {
        var allMusic = await GetAllMusicAsync();
        var max = allMusic.Max(m => m.QuentityLikes);
        var music = allMusic.First(m => m.QuentityLikes == max);
        //if (music == null) return null;
        return music;
    }

    public async Task<MusicDto> GetMusicByNameAsync(string name)
    {
        var allMusic = await GetAllMusicAsync();
        return allMusic.First(m => m.Name == name);
    }

    public async Task<List<MusicDto>> GetAllMusicAboveSizeAsync(double minSize)
    {
        var allMusic = await GetAllMusicAsync();
        return allMusic.Where(m => m.MB > minSize).ToList();
    }

    public async Task<List<MusicDto>> GetTopMostLikedMusicAsync(int count)
    {
        var allMusic = await GetAllMusicAsync();
        return allMusic.OrderByDescending(m => m.QuentityLikes)
                          .ThenBy(mu => mu.Name)
                          .Take(count)
                          .ToList();
    }

    public async Task<List<MusicDto>> GetMusicByDescriptionKeywordAsync(string keyword)
    {
        var allMusic = await GetAllMusicAsync();
        return allMusic.Where(m => m.Description.ToLower().Contains(keyword.ToLower())).ToList();
    }

    public async Task<List<MusicDto>> GetMusicWithLikesInRangeAsync(int minLikes, int maxLikes)
    {
        var allMusic = await GetAllMusicAsync();
        return allMusic.Where(m => m.QuentityLikes > minLikes && m.QuentityLikes < maxLikes).ToList();
    }

    public async Task<List<string>> GetAllUniqueAuthorsAsync()
    {
        var allMusic = await GetAllMusicAsync();

        var names = new List<string>();
        foreach (var music in allMusic)
        {
            var count = allMusic.Count(m => m.AuthorName == music.AuthorName);

            if (count == 1) names.Add(music.AuthorName);
        }
        return names;
    }

    public async Task<double> GetTotalMusicSizeByAuthorAsync(string authorName)
    {
        var allMusic = await GetAllMusicAsync();
        return allMusic.Where(m => m.AuthorName == authorName).Sum(m => m.MB);
    }

    private Music ConvertToMusicEntity(MusicDto musicDto)
    {
        return new Music
        {
            MB = musicDto.MB,
            AuthorName = musicDto.AuthorName,
            Description = musicDto.Description,
            Id = musicDto.Id ?? Guid.NewGuid(),
            Name = musicDto.Name,
            QuentityLikes = musicDto.QuentityLikes,
        };
    }

    private MusicDto ConvertToMusicDto(Music music)
    {
        return new MusicDto
        {
            QuentityLikes = music.QuentityLikes,
            Name = music.Name,
            Id = music.Id,
            AuthorName = music.AuthorName,
            Description = music.Description,
            MB = music.MB,
        };
    }
}
