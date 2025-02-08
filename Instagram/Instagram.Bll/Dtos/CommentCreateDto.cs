using Instagram.Dal.Entities;

namespace Instagram.Bll.Dtos;

public class CommentCreateDto
{
    public string Body { get; set; }

    public long AccountId { get; set; }
    public long PostId { get; set; }
    public long? ParentCommentId { get; set; }
}
