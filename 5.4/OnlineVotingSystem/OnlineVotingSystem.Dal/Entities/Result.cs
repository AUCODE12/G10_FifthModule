namespace OnlineVotingSystem.Dal.Entities;

public class Result
{
    public long Id { get; set; }
    public int TotalVotes { get; set; }
    public long ElectionId { get; set; }
    public long WinnerCandidateId { get; set; }
    public Election Election { get; set; }
    public Candidate WinnerCandidate { get; set; }
}