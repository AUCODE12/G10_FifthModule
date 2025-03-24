namespace MusicCRUD.DataAccess.Entity;

public class Comment
{
    public long CommentId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedTime { get; set; }

    public long MusicId { get; set; } // foreignKey
    public Music Music { get; set; }
}
