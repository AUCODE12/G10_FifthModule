namespace OnlineVotingSystem.Dal.Entities;

public class Campaign
{
    public long Id { get; set; }
    public string? Description { get; set; }
    public long CandidateId { get; set; }
    public Candidate Candidate { get; set; }
}