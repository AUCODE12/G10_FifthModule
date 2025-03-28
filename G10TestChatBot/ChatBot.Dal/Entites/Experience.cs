namespace ChatBot.Dal.Entites;

public class Experience
{
    public long ExperienceId { get; set; }
    public string Company { get; set; }
    public string Position { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }

    public long UserInfoId { get; set; }
    public UserInfo UserInfo { get; set; }
}
