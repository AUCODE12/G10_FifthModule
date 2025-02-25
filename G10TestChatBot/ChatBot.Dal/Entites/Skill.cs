namespace ChatBot.Dal.Entites;

public class Skill
{
    public long SkillId { get; set; }
    public string Name { get; set; }
    public string ProficiencyLevel { get; set; }

    public long UserInfoId { get; set; }
    public UserInfo UserInfo { get; set; }
}
