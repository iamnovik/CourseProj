using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProj.Models;

public class ApiToken
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Token { get; set; }
    
    public string UserId { get; set; }
    
    public virtual AppUser? AppUser { get; set; }
    
    public DateTime CreatedAt { get; set; }
}