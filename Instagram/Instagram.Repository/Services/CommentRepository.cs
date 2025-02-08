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
        //var accaunts = MainContext.Accounts
        //    .Include(a => a.Posts)
        //    .Include(a => a.Comments)
        //    .ToList();

        var accaunt = MainContext.Accounts.FirstOrDefault(a => a.AccountId == 1);

        await MainContext.Entry(accaunt)
            .Collection(a => a.Comments)
            .LoadAsync();


        foreach(var aC in accaunt.Comments)
        {
            await LoadCommentsAsync(aC);
        }


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
}
