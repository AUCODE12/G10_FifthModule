using Instagram.Bll.Dtos;

namespace Instagram.Bll.Services;

public interface IPostService
{
    Task<long> AddAsync(PostCreateDto postCreateDto);
}