using Instagram.Dal.Entities;

namespace Instagram.Bll.Dtos;

public class AccountGetDto
{
    public long AccountId { get; set; }
    public string UserName { get; set; }
    public string Bio { get; set; }
    public List<PostGetDto> PostGetDtos { get; set; }
    public List<CommentGetDto> CommentGetDtos { get; set; }
}
