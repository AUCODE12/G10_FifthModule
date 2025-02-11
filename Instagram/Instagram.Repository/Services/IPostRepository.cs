using Instagram.Dal.Entities;

namespace Instagram.Repository.Services;

public interface IPostRepository
{
    Task<long> AddPostAsync(Post post);
    Task<List<Post>> GetAllPostsAsync(bool includeComment = false);
    Task<Post> GetPostByIdAsync(long id);
    Task UpdatePostAsync(Post post);
}