using Instagram.Bll.Dtos;
using Instagram.Dal.Entities;
using Instagram.Repository.Services;

namespace Instagram.Bll.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository CommentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        CommentRepository = commentRepository;
    }

    public async Task<long> AddAsync(CommentCreateDto commentCreateDto)
    {
        var comment = new Comment()
        {
            Body = commentCreateDto.Body,
            PostId = commentCreateDto.PostId,
            AccountId = commentCreateDto.AccountId,
            ParentCommentId = commentCreateDto.ParentCommentId,
        };

        comment.CreatedTime = DateTime.UtcNow;

        return await CommentRepository.AddCommentAsync(comment);
    }

    public async Task<List<CommentGetDto>> GetAllAsync()
    {
        var comments = await CommentRepository.GetAllCommentsAsync();

        var commentGetDtos = ConvertToCommentGetDtos(comments);

        return commentGetDtos;
    }

    //private CommentGetDto ConvertToCommentGetDto(Comment comment)
    //{
    //    if(comment.Replies.Count == 0)
    //    {
    //        return new CommentGetDto()
    //        {
    //            CommentId = comment.CommentId,
    //            AccountId = comment.AccountId,
    //            Body = comment.Body,
    //            PostId = comment.PostId,
    //            CreatedTime = comment.CreatedTime,
    //            ParentCommentId = comment.ParentCommentId,
    //            Replies = new List<CommentGetDto>()
    //        };
    //    }
    //}

    private List<CommentGetDto> ConvertToCommentGetDtos(List<Comment> comments)
    {
        var commentGetDtos = new List<CommentGetDto>();
        foreach(Comment comment in comments)
        {
            var commentGetDto = new CommentGetDto()
            {
                CommentId = comment.CommentId,
                AccountId = comment.AccountId,
                Body = comment.Body,
                PostId = comment.PostId,
                CreatedTime = comment.CreatedTime,
                ParentCommentId = comment.ParentCommentId,
            };

            commentGetDtos.Add(commentGetDto);
            if (comment.Replies == null || comment.Replies.Count == 0) continue;

            commentGetDtos[commentGetDtos.Count() - 1].Replies = ConvertToCommentGetDtos(comment.Replies);
        }

        return commentGetDtos;
    }

    public Task<CommentGetDto> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }
}
