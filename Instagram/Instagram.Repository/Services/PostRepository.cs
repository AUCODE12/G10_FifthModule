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
        await MainContext.AddAsync(post);
        await MainContext.SaveChangesAsync();
        return post.PostId;
    }

    public async Task<List<Post>> GetAllPostsAsync(bool includeComment = false)
    {
        // queriable, ienimurable
        var postsQuery = MainContext.Posts.AsQueryable();

        //postsQuery = postsQuery.Where(post => post.PostType == ".png");

        if (includeComment )
        {
            postsQuery = postsQuery.Include(p => p.Comments);
        }

        //postsQuery = postsQuery.Skip(10).Take(10);

        var posts = await postsQuery.ToListAsync();    

        return posts;   
    }

    public Task<Post> GetPostByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePostAsync(Post post)
    {
        throw new NotImplementedException();
    }
}
