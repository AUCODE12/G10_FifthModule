using FluentValidation;
using Instagram.Bll.Dtos;

namespace Instagram.Server.Configurations;

public static class ValidatorConfiguration
{
    public static void ConfigureValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblyContaining<CommentCreateDto>();
        builder.Services.AddValidatorsFromAssemblyContaining<PostCreateDto>();
    }
}
