using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicManagement.Service.DTOs;
using MusicManagement.Service.Sevices;

namespace MusicManagement.Server.Controllers;

[Route("api/music")]
[ApiController]
public class MusicController : ControllerBase
{
    private readonly IMusicService _musicService;

    public MusicController(IMusicService musicService)
    {
        _musicService = musicService;
    }

    [HttpPost("add")]
    public async Task<Guid> AddMusic(MusicDto musicDto)
    {
        return await _musicService.AddMusicAsync(musicDto);
    }

    [HttpGet("getAll")]
    public async Task<List<MusicDto>> GetAllMusic()
    {
        return await _musicService.GetAllMusicAsync();
    }

    [HttpGet("getById/{id}")]
    public async Task<MusicDto> GetByIdMusic(Guid id)
    {
        return await _musicService.GetMusicByIdAsync(id);
    }

    [HttpPut("update")]
    public async Task PutMusic(MusicDto musicDto)
    {
        await _musicService.UpdateMusicAsync(musicDto);
    }

    [HttpDelete("delete/{id}")]
    public async Task DeleteMusic(Guid id)
    {
        await _musicService.DeleteMusicAsync(id);
    }

    [HttpGet("getByAuthor/{authorName}")]
    public async Task<List<MusicDto>> GetAllMusicByAuthorName(string authorName)
    {
        return await _musicService.GetAllMusicByAuthorNameAsync(authorName);
    }

    [HttpGet("getMostLiked")]
    public async Task<MusicDto> GetMostLikedMusicAsync()
    {
        return await _musicService.GetMostLikedMusicAsync();
    }

    [HttpGet("getByName/{name}")]
    public async Task<MusicDto> GetMusicByNameAsync(string name)
    {
        return await _musicService.GetMusicByNameAsync(name);
    }

    [HttpGet("getAboveSize/{minSize}")]
    public async Task<List<MusicDto>> GetAllMusicAboveSizeAsync(double minSize)
    {
        return await _musicService.GetAllMusicAboveSizeAsync(minSize);
    }

    [HttpGet("getTopMostLiked/{count}")]
    public async Task<List<MusicDto>> GetTopMostLikedMusicAsync(int count)
    {
        return await _musicService.GetTopMostLikedMusicAsync(count);
    }

    [HttpGet("getDescriptionContainKeyword/{keyword}")]
    public async Task<List<MusicDto>> GetMusicByDescriptionKeywordAsync(string keyword)
    {
        return await _musicService.GetMusicByDescriptionKeywordAsync(keyword);
    }

    [HttpGet("getWithLikeInRange")]
    public async Task<List<MusicDto>> GetMusicWithLikesInRangeAsync(int minLikes, int maxLikes)
    {
        return await _musicService.GetMusicWithLikesInRangeAsync(minLikes, maxLikes);
    }

    [HttpGet("getUniqueAuthor")]
    public async Task<List<string>> GetAllUniqueAuthorsAsync()
    {
        return await _musicService.GetAllUniqueAuthorsAsync();
    }

    [HttpGet("GetTotalSizeByAuthor/{authorName}")]
    public async Task<double> GetTotalMusicSizeByAuthorAsync(string authorName)
    {
        return await _musicService.GetTotalMusicSizeByAuthorAsync(authorName);
    }
}
