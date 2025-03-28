namespace ChatBot.Dal.Entites;

public class Education
{
    public long EducationId { get; set; }
    public string Institution { get; set; }
    public string Degree { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
        
    public long UserInfoId { get; set; }
    public UserInfo UserInfo { get; set; }
}
