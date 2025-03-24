namespace MusicCRUD.DataAccess.Entity;

public class UserDetail
{
    public long UserDetailId { get; set; }
    public int Age { get; set; }

    public long UserId { get; set; } // foreign key
    public User User { get; set; }
}
