using FluentValidation;
using Instagram.Bll.Dtos;
using Instagram.Repository.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Instagram.Bll.Validators;

public class CommentCreateDtoValidator : AbstractValidator<CommentCreateDto>
{
    private readonly ICommentRepository CommentRepository;
    public CommentCreateDtoValidator(ICommentRepository commentRepository)
    {
        CommentRepository = commentRepository;
        RuleFor(c => c.Body)
            .MaximumLength(200).WithMessage("Body len must be less then 200");

        RuleFor(c => c.ParentCommentId)
            .MustAsync(ParentCommentExists).WithMessage("Parent comment id not found");
    }
    
    private async Task<bool> ParentCommentExists(long? id, CancellationToken token)
    {
        if (id == null) return true;
        return await CommentRepository.CommentExistsAsync(id.Value);
    }
}
