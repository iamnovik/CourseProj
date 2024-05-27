using Microsoft.AspNetCore.Identity;

namespace CourseProj.Models;

public class AppUser : IdentityUser
{
    public string? Name { get; set; }
    
    public bool IsBlocked { get; set; } = false;
    public virtual ICollection<Collection> Collections { get; set; }
    public virtual ICollection<Like> Likes { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
}