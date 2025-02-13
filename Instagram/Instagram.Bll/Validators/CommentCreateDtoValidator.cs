using FluentValidation;
using Instagram.Bll.Dtos;
using Instagram.Repository.Services;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Bll.Validators;

public class CommentCreateDtoValidator : AbstractValidator<CommentCreateDto>
{
    private readonly ICommentRepository CommentRepository;

    public CommentCreateDtoValidator(ICommentRepository commentRepository)
    {
        CommentRepository = commentRepository;

        RuleFor(c => c.Body)
            .NotEmpty().WithMessage("Comment body is required.")
            .MaximumLength(200).WithMessage("Comment body cannot exceed 200 characters.");

        RuleFor(c => c.ParentCommentId)
            .MustAsync(ParentCommentExists).WithMessage("Parent comment does not exist.")
            .When(c => c.ParentCommentId.HasValue);

        RuleFor(c => c.AccountId)
            .GreaterThanOrEqualTo(1).WithMessage("Account id should be 0 <")
            .Must(true).WithMessage("");
    }
    
    private async Task<bool> ParentCommentExists(long? parentCommentId, CancellationToken cancellationToken)
    {
        if(parentCommentId == null) return true;
        return await CommentRepository.CommentExistsAsync(parentCommentId.Value);
    }
}
