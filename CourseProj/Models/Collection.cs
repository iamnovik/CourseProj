using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseProj.Models.Enums;
using NpgsqlTypes;

namespace CourseProj.Models;

public class Collection
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    public int CategoryId { get; set; }

    public string? ImageUrl { get; set; }
    
    public string? AppUserId { get; set; }
    public virtual AppUser? AppUser { get; set; }
    
    public  virtual Category Category { get; set; }

    public virtual ICollection<CollectionItemAttribute> ItemsAttributes { get; set; }

    public virtual ICollection<Item> Items { get; set; }
    

    public NpgsqlTsVector SearchVector { get; set; }
}