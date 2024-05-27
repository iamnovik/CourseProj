namespace CourseProj.Models;

public class Like
{
    public int ItemId { get; set; }
    public Item Item { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser{ get; set; }
}