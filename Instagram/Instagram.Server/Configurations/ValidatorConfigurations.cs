using FluentValidation;
using Instagram.Bll.Validators;

namespace Instagram.Server.Configurations;

public static class ValidatorConfigurations
{
    public static void ConfigureValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblyContaining<CommentCreateDtoValidator>();
    }
}
