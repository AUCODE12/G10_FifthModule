using Instagram.Dal.Entities;
using System.Net;

namespace Instagram.Repository.Services;

public interface ICommentRepository
{
    Task<long> AddCommentAsync(Comment comment);
    Task<Comment> GetCommentByIdAsync(long id);
    Task<List<Comment>> GetAllCommentsAsync();
}