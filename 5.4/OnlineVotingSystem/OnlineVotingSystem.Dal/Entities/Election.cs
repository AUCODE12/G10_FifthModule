namespace OnlineVotingSystem.Dal.Entities;

public class Election
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Result? Result { get; set; }
    public ICollection<Candidate> Candidates { get; set; }
    public ICollection<Vote> Votes { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public ICollection<Debate> Debates { get; set; }
}
