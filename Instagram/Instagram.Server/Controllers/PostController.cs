using Instagram.Bll.Dtos;
using Instagram.Bll.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Server.Controllers;

[Route("api/post")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService PostService;

    public PostController(IPostService postService)
    {
        PostService = postService;
    }

    [HttpPost("add")]
    public async Task<long> AddPost(PostCreateDto postCreateDto)
    {
        return await PostService.AddAsync(postCreateDto);
    }
}
