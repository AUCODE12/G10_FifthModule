namespace OnlineVotingSystem.Dal.Entities;

public class Debate
{
    public long Id { get; set; }
    public string Topic { get; set; }
    public DateTime Date { get; set; }
    public long ElectionId { get; set; }
    public Election Election { get; set; }
}