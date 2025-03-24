using MusicManagement.Service.DTOs;

namespace MusicManagement.Service.Sevices;

public interface IMusicService
{
    Task<long> AddMusicAsync(MusicDto musicDto);
    Task DeleteMusicAsync(Guid id);
    Task UpdateMusicAsync(MusicDto updateMusicDto);
    Task<MusicDto> GetMusicByIdAsync(Guid id);
    Task<List<MusicDto>> GetAllMusicAsync();
    Task<List<MusicDto>> GetAllMusicByAuthorNameAsync(string name);
    Task<MusicDto> GetMostLikedMusicAsync();
    Task<MusicDto> GetMusicByNameAsync(string name);
    Task<List<MusicDto>> GetAllMusicAboveSizeAsync(double minSize);
    Task<List<MusicDto>> GetTopMostLikedMusicAsync(int count);
    Task<List<MusicDto>> GetMusicByDescriptionKeywordAsync(string keyword);
    Task<List<MusicDto>> GetMusicWithLikesInRangeAsync(int minLikes, int maxLikes);
    Task<List<string>> GetAllUniqueAuthorsAsync();
    Task<double> GetTotalMusicSizeByAuthorAsync(string authorName);
}