using Instagram.Dal;
using Instagram.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Repository.Services;

public class PostRepository : IPostRepository
{
    private readonly MainContext MainContext;

    public PostRepository(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public async Task<long> AddPostAsync(Post post)
    {
        await MainContext.Posts.AddAsync(post);
        await MainContext.SaveChangesAsync();
        return post.PostId;
    }

    public async Task<List<Post>> GetAllPostsAsync(bool includeComment = false)
    {
        // queriable, ienimurable
        var postsQuery = MainContext.Posts.AsQueryable();

    public async Task<List<Post>> GetAllPostsAsync()
        {
        return await MainContext.Posts
            .Include(p => p.Comments)
            .ToListAsync();   
    }
}
