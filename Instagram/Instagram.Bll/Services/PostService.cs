using AutoMapper;
using FluentValidation;
using Instagram.Bll.Dtos;
using Instagram.Dal.Entities;
using Instagram.Repository.Services;

namespace Instagram.Bll.Services;

public class PostService : IPostService
{
    private readonly IPostRepository PostRepository;
    private readonly IValidator<PostCreateDto> PostCreateDtoValidator;
    private readonly IMapper Mapper;

    public PostService(IPostRepository postRepository,
        IValidator<PostCreateDto> postCreateDtoValidator,
        IMapper mapper)
    {
        PostRepository = postRepository;
        PostCreateDtoValidator = postCreateDtoValidator;
        Mapper = mapper;
    }

    public async Task<long> AddAsync(PostCreateDto postCreateDto)
    {
        var validatorRes = await PostCreateDtoValidator.ValidateAsync(postCreateDto);

        if (validatorRes.IsValid == false)
        {
            throw new ValidationException($"{string.Join(',', validatorRes.Errors)}");
        }

        var post = Mapper.Map<Post>(postCreateDto);

        return await PostRepository.AddPostAsync(post);
    }
}
