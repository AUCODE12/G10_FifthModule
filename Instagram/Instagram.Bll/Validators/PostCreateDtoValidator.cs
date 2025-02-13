using FluentValidation;
using Instagram.Bll.Dtos;
using Instagram.Repository.Services;

namespace Instagram.Bll.Validators;

public class PostCreateDtoValidator : AbstractValidator<PostCreateDto>
{
    public PostCreateDtoValidator()
    {
        RuleFor(c => c.PostType)
            .MaximumLength(20).WithMessage("Post type len must be less then 20")
            .NotEmpty().WithMessage("Post type can not be empty");

        RuleFor(c => c.AccountId)
            .GreaterThanOrEqualTo(1).WithMessage("Account id can not be less then 1");
    }
}
