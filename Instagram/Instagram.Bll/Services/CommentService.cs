using FluentValidation;
using Instagram.Bll.Dtos;
using Instagram.Dal.Entities;
using Instagram.Repository.Services;
using System.Linq;

namespace Instagram.Bll.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository CommentRepository;
    private readonly IValidator<CommentCreateDto> CommentCreateDtoValidator;

    public CommentService(ICommentRepository commentRepository, IValidator<CommentCreateDto> commentCreateDtoValidator)
    {
        CommentRepository = commentRepository;
        CommentCreateDtoValidator = commentCreateDtoValidator;
    }

    public async Task<long> AddAsync(CommentCreateDto commentCreateDto)
    {
        var validationResult = await CommentCreateDtoValidator.ValidateAsync(commentCreateDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var comment = new Comment()
        {
            throw new ValidationException($"{string.Join(',', validatorRes.Errors)}");
        }

        var comment = Mapper.Map<Comment>(commentCreateDto);
        comment.CreatedTime = DateTime.UtcNow;

        return await CommentRepository.AddCommentAsync(comment);
    }

    public async Task<List<CommentGetDto>> GetAllAsync()
    {
        var comments = await CommentRepository.GetAllCommentsAsync();

        //var commentGetDtos = ConvertToCommentGetDtos(comments);

        var commentGetDtos = comments.Select(c => Mapper.Map<CommentGetDto>(c)).ToList();

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
        foreach (Comment comment in comments)
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
