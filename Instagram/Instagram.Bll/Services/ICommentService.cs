using Instagram.Bll.Dtos;

namespace Instagram.Bll.Services;

public interface ICommentService
{
    Task<long> AddAsync(CommentCreateDto commentCreateDto);

    Task<CommentGetDto> GetByIdAsync(long id);

    Task<List<CommentGetDto>> GetAllAsync();
}