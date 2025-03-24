namespace OnlineVotingSystem.Dal.Entities;

public class Comment
{
    public long Id { get; set; }
    public string Massage { get; set; }
    public long UserId { get; set; }
    public long ElectionId { get; set; }   
    public User User { get; set; }
    public Election Election { get; set; }
}