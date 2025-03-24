using MucisCRUD.Service.DTOs;

namespace MucisCRUD.Service.Service;

public interface IMusicService
{
    Task<long> AddMusicAsync(MusicDto musicDto);
    Task DeleteMusicAsync(long id);
    Task<List<MusicDto>> GetAllMusicAsync();
    Task UpdateMusicAsync(MusicDto musicDto);
    Task<MusicDto> GetMusicById(long id);
    Task<List<MusicDto>> GetAllMusicByAuthorNameAsync(string name);
    Task<MusicDto> GetMostLikedMusicAsync();
    Task<MusicDto> GetMusicByNameAsync(string name);
    Task<List<MusicDto>> GetAllMusicAboveSizeAsync(double minSizeMB);
    Task<List<MusicDto>> GetAllMusicBelowSizeAsync(double maxSizeMB);
    Task<List<MusicDto>> GetTopMostLikedMusicAsync(int count);
    Task<List<MusicDto>> GetLowMostLikedMusicAsync(int count);
    Task<List<MusicDto>> GetMusicByDescriptionKeywordAsync(string keyword);
    Task<List<MusicDto>> GetMusicWithLikesInRangeAsync(int minLikes, int maxLikes);

    /// <summary>
    /// This method returns collection of Author unique names.
    /// Author unique is author who created only single music.
    /// </summary>
    /// <returns></returns>
    Task<List<string>> GetAllUniqueAuthorsAsync();
    Task<double> GetTotalMusicSizeByAuthorAsync(string authorName);
}