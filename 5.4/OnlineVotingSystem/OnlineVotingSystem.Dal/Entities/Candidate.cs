namespace OnlineVotingSystem.Dal.Entities;

public class Candidate
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Party { get; set; }
    public long ElectionId { get; set; }
    public Election Election { get; set; }
    public ICollection<Vote> Votes { get; set; }
    public ICollection<Campaign> Campaigns { get; set; }
}
