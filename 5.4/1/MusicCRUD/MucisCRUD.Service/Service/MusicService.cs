using MucisCRUD.Service.DTOs;
using MusicCRUD.DataAccess.Entity;
using MusicCRUD.Repository.Services;

namespace MucisCRUD.Service.Service;

public class MusicService : IMusicService
{
    private readonly IMusicRepository _musicRepository;

    public MusicService(IMusicRepository musicRepository)
    {
        _musicRepository = musicRepository;
    }

    public async Task<long> AddMusicAsync(MusicDto musicDto)
    {
        var music = ConvertToMusicEntity(musicDto);
        var idRes = await _musicRepository.AddMusicAsync(music);
        return idRes;
    }

    public async Task DeleteMusicAsync(long id)
    {
        await _musicRepository.DeleteMusicAsync(id);
    }

    public async Task<List<MusicDto>> GetAllMusicAsync()
    {
        var music = await _musicRepository.GetAllMusicAsync();
        var musicDtos = music.Select(mu => ConvertToMusicDto(mu)).ToList();
        return musicDtos;
    }

    public async Task UpdateMusicAsync(MusicDto musicDto)
    {
        var music = ConvertToMusicEntity(musicDto);
        await _musicRepository.UpdateMusicAsync(music);
    }

    private Music ConvertToMusicEntity(MusicDto musicDto)
    {
        return new Music()
        {
            MusicId = musicDto.MusicId ?? 0,
            Name = musicDto.Name,
            MB = musicDto.MB,
            AuthorName = musicDto.AuthorName,
            Description = musicDto.Description,
            QuentityLikes = musicDto.QuentityLikes,
        };
    }

    private MusicDto ConvertToMusicDto(Music music)
    {
        return new MusicDto()
        {
            MusicId = music.MusicId,
            Name = music.Name,
            MB = music.MB,
            AuthorName = music.AuthorName,
            Description = music.Description,
            QuentityLikes = music.QuentityLikes,
        };
    }

    public async Task<List<MusicDto>> GetAllMusicByAuthorNameAsync(string name)
    {
        var music = await GetAllMusicAsync();
        var resMusic = music.Where(mu => mu.AuthorName.ToLower() == name.ToLower()).ToList();

        return resMusic;
    }

    public async Task<MusicDto> GetMostLikedMusicAsync()
    {
        var music = await GetAllMusicAsync();
        var amountLikes = music.Max(mu => mu.QuentityLikes);
        var mostLikedMusic = music.FirstOrDefault(mu => mu.QuentityLikes == amountLikes);

        if (mostLikedMusic == null)
        {
            throw new NullReferenceException("Storage is empty");
        }

        return mostLikedMusic;
    }

    public async Task<MusicDto> GetMusicByNameAsync(string name)
    {
        var music = await GetAllMusicAsync();
        var musicByName = music.FirstOrDefault(mu => mu.Name.ToLower() == name.ToLower());

        if (musicByName == null)
        {
            throw new NullReferenceException("Storage is empty");
        }

        return musicByName;
    }

    public async Task<List<MusicDto>> GetAllMusicAboveSizeAsync(double minSizeMB)
    {
        var music = await GetAllMusicAsync();
        var musicAboveSize = music.Where(mu => mu.MB > minSizeMB).ToList();

        return musicAboveSize;
    }

    public async Task<List<MusicDto>> GetAllMusicBelowSizeAsync(double maxSizeMB)
    {
        var music = await GetAllMusicAsync();
        var musicBelowSize = music.Where(mu => mu.MB < maxSizeMB).ToList();

        return musicBelowSize;
    }

    public async Task<List<MusicDto>> GetTopMostLikedMusicAsync(int count)
    {
        var music = await GetAllMusicAsync();
        var topMostLikedMusic = music.OrderByDescending(mu => mu.QuentityLikes)
                                     .ThenBy(mu => mu.Name)
                                     .Take(count)
                                     .ToList();

        return topMostLikedMusic;
    }

    public async Task<List<MusicDto>> GetLowMostLikedMusicAsync(int count)
    {
        var music = await GetAllMusicAsync();
        var lowMostLikedMusic = music.OrderByDescending(mu => mu.QuentityLikes)
                                     .ThenBy(mu => mu.Name)
                                     .TakeLast(count)
                                     .ToList();

        return lowMostLikedMusic;
    }

    public async Task<List<MusicDto>> GetMusicByDescriptionKeywordAsync(string keyword)
    {
        var music = await GetAllMusicAsync();
        var res = music.Where(mu => mu.Description.ToLower().Contains(keyword.ToLower()))
                       .ToList();

        return res;
    }

    public async Task<List<MusicDto>> GetMusicWithLikesInRangeAsync(int minLikes, int maxLikes)
    {
        var music = await GetAllMusicAsync();
        var res = music.Where(mu => minLikes <= mu.QuentityLikes && mu.QuentityLikes <= maxLikes)
                       .ToList();

        return res;
    }

    public async Task<List<string>> GetAllUniqueAuthorsAsync()
    {
        var music = await GetAllMusicAsync();

        var names = new List<string>(); 
        foreach (var mus in music)
        {
            var count = music.Count(mu => mu.AuthorName == mus.AuthorName);

            if(count == 1) names.Add(mus.AuthorName);
        }

        return names;
    }

    public async Task<double> GetTotalMusicSizeByAuthorAsync(string authorName)
    {
        var music = await GetAllMusicAsync();
        var res = music.Where(mu => mu.AuthorName.ToLower() == authorName.ToLower())
                       .Sum(m => m.MB);

        return res;
    }

    public async Task<MusicDto> GetMusicById(long id)
    {
        return ConvertToMusicDto(await _musicRepository.GetMusicByIdAsync(id));
    }
}
