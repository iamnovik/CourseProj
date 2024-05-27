using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NpgsqlTypes;

namespace CourseProj.Models;

public class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int ItemId { get; set; }
    public Item Item { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser{ get; set; }
    
    public string Text { get; set; }
    
    public NpgsqlTsVector SearchVector { get; set; }
}