using Instagram.Dal;
using Instagram.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Instagram.Repository.Services;

public class CommentRepository : ICommentRepository
{
    private readonly MainContext MainContext;

    public CommentRepository(MainContext mainContext)
    {
        MainContext = mainContext;
    }

    public async Task<long> AddCommentAsync(Comment comment)
    {
        await MainContext.Comments.AddAsync(comment);
        await MainContext.SaveChangesAsync();

        return comment.CommentId;
    }

    public async Task<List<Comment>> GetAllCommentsAsync()
    {
        var comments = await MainContext.Comments.ToListAsync();

        return comments;
    }

    private async Task LoadCommentsAsync(Comment comment)
    {
        if(comment == null) return;

        await MainContext.Entry(comment)
            .Collection(a => a.Replies)
            .LoadAsync();

        if (comment.Replies == null) return;

        foreach (var c in comment.Replies)
        {
            await LoadCommentsAsync(c);
        }
    }

    public async Task<Comment> GetCommentByIdAsync(long id)
    {
        var comment = await MainContext.Comments
            .FirstOrDefaultAsync(c => c.CommentId == id);

        if(comment == null)
        {
            throw new Exception("Null");
        }

        return comment;
    }

    public async Task<bool> CommentExistsAsync(long id)
    {
        var res = await MainContext.Comments.AnyAsync(c => c.CommentId == id);
        
        return res;
    }
}
