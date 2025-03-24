namespace OnlineVotingSystem.Dal.Entities;

public class Notification
{
    public long Id { get; set; }
    public string Message { get; set; }
    public bool ReadStatus { get; set; } = false;
    public long UserId { get; set; }
    public User User { get; set; }
}