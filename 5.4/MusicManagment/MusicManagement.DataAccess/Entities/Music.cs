namespace MusicManagement.DataAccess.Entities;

public class Music
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public double MB { get; set; }
    
    public string AuthorName { get; set; }
    
    public string Description { get; set; }

    public int QuentityLikes { get; set; }
}
