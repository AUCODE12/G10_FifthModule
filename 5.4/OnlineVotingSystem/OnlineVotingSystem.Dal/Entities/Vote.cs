namespace OnlineVotingSystem.Dal.Entities;

public class Vote
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long CandidateId { get; set; }
    public long ElectionId { get; set; }
    public User User { get; set; }
    public Candidate Candidate { get; set; }
    public Election Election { get; set; }
}